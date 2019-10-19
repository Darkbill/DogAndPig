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
	private int exp = 0;
	public float degree;
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
		calStat = JsonMng.Ins.playerDataTable[1];
		CalculatorStat();
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
		if (degree > 90 || degree < -90)
		{
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else transform.eulerAngles = new Vector3(0, 0, 0);

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
	#region Damage
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
		ChangeAnimation(ePlayerAnimation.Damage);
		if (d < 1) d = 1;
		calStat.healthPoint -= d;
		UIMngInGame.Ins.DamageToPlayer(d);
		if (calStat.healthPoint <= 0) GameMng.Ins.GameOver();
	}
	/* Damage */
	#endregion
	#region Buff
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
		if (conditionMain != null)
		{
			conditionMain.currentTime -= delayTime;
			if (conditionMain.currentTime <= 0)
			{
				Debug.Log("상태이상이 풀렸습니다.");
				conditionMain = null;
				playerStateMachine.ChangeState(ePlayerState.Move);
			}
		}
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
					ChangeState(conditionMain.buffType);
                }
                return;
            }
        }
        conditionMain = condition;
		ChangeState(conditionMain.buffType);
    }

    private void ChangeState(eBuffType stateType)
    {
		switch(stateType)
		{
			case eBuffType.NockBack:
				playerStateMachine.ChangeState(ePlayerState.KnockBack);
				break;
			case eBuffType.Stun:
				playerStateMachine.ChangeState(ePlayerState.Stun);
				break;
		}
        Debug.Log("상태이상에 걸렸습니다.");
    }


	/* Buff */
	#endregion
	private void CalculatorStat()
	{
		//TODO : 레벨에 의한 스탯계산
		calStat = calStat.AddStat(skillStat,conditionList);
	}
	public void AddEXP(int _exp)
	{
		exp += _exp;
		if(exp >= JsonMng.Ins.expDataTable[calStat.level+1].requiredExp)
		{
			int saveEXP = JsonMng.Ins.expDataTable[calStat.level + 1].requiredExp - exp;
			LevelUP();
			exp = saveEXP;
		}
	}
	public float GetFullHP()
	{
		return skillStat.healthPoint + calStat.GetHealthPoint(calStat.level);
	}
	public float GetEXPFill()
	{
		return (float)exp / JsonMng.Ins.expDataTable[calStat.level + 1].requiredExp;
	}
	private void LevelUP()
	{
		calStat.level++;
		exp = 0;
		CalculatorStat();
		UIMngInGame.Ins.RenewPlayerInfo();
	}
	public void ChangeAnimation(ePlayerAnimation animationType)
	{
		playerAnimator.SetInteger("Action", (int)animationType);
	}
}
