using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public abstract class Monster : MonoBehaviour
{
	public MonsterData monsterData;
	public Condition condition;

	/* 테스트코드 */
	private int MonsterID = 1;
	private ConditionData[] conditionArr = new ConditionData[(int)eBuffType.Max];
	public void Start()
	{
		MonsterSetting();
	}
	public void MonsterSetting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[MonsterID];
		for (int i = 0; i < conditionArr.Length; ++i)
		{
			conditionArr[i] = new ConditionData();
		}
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
			conditionArr[(int)condition.buffType].SetBuff(condition.SustainmentTime, condition.changeValue);
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

	public virtual void Dead()
	{
		Debug.Log("죽었다");
		//TODO : 경험치 추가
	}

}
