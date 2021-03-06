﻿using GlobalDefine;
public class PlayerData
{
	public float size;
	public float healthPoint;
	public float damage;
	public float attackRange;
	public float attackAngle;
	public float attackSpeed;
	public float moveSpeed;
	public float criticalChance;
	public float criticalDamage;
	public float armor;
	public float physicsResist;
	public float fireResist;
	public float waterResist;
	public float windResist;
	public float lightningResist;
    public float knockback = 0;
    public float stun = 0;

    public PlayerData()
	{

	}
	public float GetResist(eAttackType attackType)
	{
		switch(attackType)
		{
			case eAttackType.Physics:
				return physicsResist;
			case eAttackType.Fire:
				return fireResist;
			case eAttackType.Water:
				return waterResist;
			case eAttackType.Lightning:
				return lightningResist;
			default:
				return 0;
		}
	}
	public int GetHealthPoint(int lv)
	{
		return (int)((lv * JsonMng.Ins.playerDataTable.healthPoint) + ((lv + 1) * (lv - 1)));
	}
}
