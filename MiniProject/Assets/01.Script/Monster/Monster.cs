using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class Monster : MonoBehaviour
{
	public MonsterData monsterData;

	/* 테스트코드 */
	private int MonsterID = 1;
	private List<ConditionData> conditionList = new List<ConditionData>();
	public void Start()
	{
		MonsterSetting();
		AddBuff(new ConditionData(eBuffType.MoveSlow, 1, 2, 0.5f));
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
	}
	public void MonsterSetting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[MonsterID].Copy();
	}

	/* 테스트코드 */
	public virtual void Attack()
	{
		GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
	}
	public void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float damage, ConditionData condition)
	{
		float d = (damage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		bool isBuff = monsterData.GetResist(attackType).GetBuff();
		if (isBuff)
		{
			AddBuff(condition);
		}
		DamageResult((int)d);
	}
	public void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
	}

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
	}
	public void AddBuff(ConditionData condition)
	{
		int index = conditionList.FindID(condition.skillIndex);
		if (index != -1) conditionList[index].Set(condition);
		else conditionList.Add(condition);
		CalculatorStat();
	}
	private void CalculatorStat()
	{
		MonsterSetting();
		for (int i = 0; i < conditionList.Count; ++i)
		{
			switch (conditionList[i].buffType)
			{
				case eBuffType.MoveFast:
					monsterData.moveSpeed += conditionList[i].changeValue;
					break;
				case eBuffType.MoveSlow:
					monsterData.moveSpeed -= conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsStrong:
					monsterData.damage += conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsWeek:
					monsterData.damage -= conditionList[i].changeValue;
					break;
			}
		}
	}
	public virtual void Dead()
	{
		Debug.Log("죽었다");
		//TODO : 경험치 추가
	}

}
