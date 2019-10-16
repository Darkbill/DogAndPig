using GlobalDefine;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
	// * public * //
	public bool isMove;
	public PlayerData calStat;
	public PlayerData skillStat = new PlayerData();
	public PlayerStateMachine playerStateMachine;
	public SpriteRenderer playerSprite;
	// * private * //
	private int level = 1;

	private List<ConditionData> conditionList = new List<ConditionData>();

    /* 테스트 코드 */
    public int[] skillArr;

    public bool[] specialAttack = new bool[2];

    private void Awake()
	{
		skillArr[0] = 1;
		skillArr[1] = 2;
		skillArr[2] = 3;
        skillArr[3] = 4;
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
	/* Buff */

	private void CalculatorStat()
	{
		//TODO : 레벨에 의한 스탯계산
		calStat = JsonMng.Ins.playerDataTable[1].AddStat(skillStat,conditionList);
	}

	public float GetFullHP()
	{
		return skillStat.healthPoint + JsonMng.Ins.playerDataTable[level].healthPoint;
	}
	private void LevelUP()
	{
		CalculatorStat();
	}
}
