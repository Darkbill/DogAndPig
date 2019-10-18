using GlobalDefine;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
	// * public * //
	public bool isMove;
	public PlayerData calStat;
	public PlayerStateMachine playerStateMachine;
	public Sprite playerSprite;
	public Animator playerAnimator;
	// * private * //
	private List<ConditionData> conditionList = new List<ConditionData>();
    private ConditionData conditionMain = new ConditionData();
	private int level = 1;
	private int exp = 0;
	private PlayerData skillStat = new PlayerData();
	private void Awake()
	{
		PlayerSetting();
	}
	/* 테스트 코드 */
	public float GetTime(int skillIndex)
	{
		return conditionList[conditionList.FindID(skillIndex)].currentTime;
	}
	private void PlayerSetting()
	{
		CalculatorStat();
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);

		if (isMove)
		{
			MoveToJoyStick();
		}
	}
    public void MoveToJoyStick()
	{
		Vector3 direction = UIMngInGame.Ins.GetJoyStickDirection();
		gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * calStat.moveSpeed * Time.deltaTime;

        float Degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, Degree);
	}

	/* Damage */
	public void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - calStat.armor) * calStat.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float damage, ConditionData condition,float activePer)
	{
		float d = (damage - calStat.armor) * calStat.GetResist(attackType).CalculatorDamage();
		bool isBuff = calStat.GetResist(attackType).GetBuff(activePer);
		if(isBuff)
		{
			AddBuff(condition);
		}
		DamageResult((int)d);
	}
	public void DamageResult(int d)
	{
		playerAnimator.SetInteger("Action", (int)ePlayerAnimation.Damage);
		if (d < 1) d = 1;
		calStat.healthPoint -= d;
		UIMngInGame.Ins.DamageToPlayer(d);
		if (calStat.healthPoint <= 0) GameMng.Ins.GameOver();
	}
	/* Damage */

	/* Buff */
	private void UpdateBuff(float delayTime)
	{
		for (int i = 0; i < conditionList.Count; ++i)
		{
			conditionList[i].currentTime -= delayTime;
			if (conditionList[i].currentTime <= 0)
			{
				conditionList.Remove(conditionList[i]);
				CalculatorStat();
			}
		}
        StateBuffUpdate();
	}

    //TODO : 현재 적용된 상태이상 버프에 대한 확률 값.
    private void StateBuffUpdate()
    {
        if(calStat.stun > Rand.Random() % 1000)
        {
            calStat.stunAtt = true;
            return;
        }
        //기절이 걸린 상태이면 다음 상태인 넉백으로 넘어 갈 필요가 없다.
        if (calStat.knockback > Rand.Random() % 1000)
        {
            calStat.knockbackAtt = true;
            return;
        }
        calStat.ResetAttackType();
    }

    public void AddBuff(ConditionData condition)
	{
        int index = conditionList.FindID(condition.skillIndex, condition.buffType);
        if (index != -1) { conditionList[index].Set(condition); return; }
        else conditionList.Add(condition);
        CalculatorStat();
		UIMngInGame.Ins.ActiveBuff(condition.skillIndex);
	}

    public void OutStateAdd(ConditionData condition, float activePer)
    {
        if (conditionMain != null)
        {
            if (conditionMain.buffType == condition.buffType)
            {
                if (conditionMain.currentTime < condition.currentTime)
                {
                    conditionMain = condition;
                    AddConditionlist(conditionMain);
                }
                return;
            }
        }
        conditionMain = condition;
        AddConditionlist(conditionMain);
    }

    private void AddConditionlist(ConditionData condition)
    {
        switch (condition.buffType)
        {
            case eBuffType.Stun:
                gameObject.GetComponent<PlayerStateMachine>().ChangeState(ePlayerState.Stun);
                break;
            case eBuffType.NockBack:
                gameObject.GetComponent<PlayerStateMachine>().ChangeState(ePlayerState.KnockBack);
                break;
        }
        Debug.Log("상태이상에 걸렸습니다.");
    }

    public void OutStateUpdate(float delayTime)
    {
        if (conditionMain != null)
        {
            conditionMain.currentTime -= delayTime;
            if (conditionMain.currentTime <= 0)
            {
                Debug.Log("상태이상이 풀렸습니다.");
                conditionMain = null;
                gameObject.GetComponent<PlayerStateMachine>().ChangeState(ePlayerState.Move);
            }
        }
    }

    /* Buff */
    private void CalculatorStat()
	{
		//TODO : 레벨에 의한 스탯계산
		calStat = JsonMng.Ins.playerDataTable[level].AddStat(skillStat,conditionList);
	}
	public void AddEXP(int _exp)
	{
		exp += _exp;
		if(exp >= JsonMng.Ins.expDataTable[level+1].requiredExp)
		{
			int saveEXP = JsonMng.Ins.expDataTable[level+1].requiredExp - exp;
			LevelUP();
			exp = saveEXP;
		}
	}
	public float GetFullHP()
	{
		return skillStat.healthPoint + JsonMng.Ins.playerDataTable[level].healthPoint;
	}
	public float GetEXPFill()
	{
		return (float)exp / JsonMng.Ins.expDataTable[level+1].requiredExp;
	}
	private void LevelUP()
	{
		level++;
		exp = 0;
		CalculatorStat();
	}
}
