using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public abstract class Monster : MonoBehaviour
{
	public MonsterData monsterData;
    public UnitState state = new UnitState();

    /* 테스트코드 */
    private int MonsterID = 1;
	public void Start()
	{
		MonsterSetting();
	}
	public void MonsterSetting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[MonsterID];
	}

	/* 테스트코드 */
	public virtual void Attack()
	{
		GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
	}
	public virtual void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - monsterData.armor);
		float resist;
		switch (attackType)
		{
			case eAttackType.Physics:
				resist = monsterData.physicsResist;
				GetDamage(resist);
				break;
			case eAttackType.Fire:
				resist = monsterData.fireResist;
				GetDamage(resist);
				break;
			case eAttackType.Water:
				resist = monsterData.waterResist;
				GetDamage(resist);
				break;
			case eAttackType.Wind:
				resist = monsterData.windResist;
				GetDamage(resist);
				break;
			case eAttackType.Lightning:
				resist = monsterData.lightningResist;
				GetDamage(resist);
				break;
		}
		if (d < 1) d = 1;
		//TODO : 몬스터체력
		monsterData.healthPoint -= (int)d;
		UIMngInGame.Ins.ShowDamage((int)d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
	}
	public int GetDamage(float resist)
	{
		if (resist >= 500)
		{
			return (int)(((1500 - monsterData.physicsResist) * 0.001) * 1.6f - 0.6f);
		}
		else
		{
			return (int)(((500 - monsterData.physicsResist) * 0.001) * 8 - 1);
		}
	}

	public virtual void Dead()
	{
		Debug.Log("죽었다");
		//TODO : 경험치 추가
	}

}
