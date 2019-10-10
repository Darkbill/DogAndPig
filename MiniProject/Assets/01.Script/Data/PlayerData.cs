using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : TableBase
{
	public float level;
	public float size;
	public float healthPoint;
	public float damage;
	public float attackRange;
	public float attackAngle;
	public float attackSpeed;
	public float moveSpeed;
	public float criticalChange;
	public float criticalDamage;
	public float armor;
	public float physicsResist;
	public float fireResist;
	public float waterResist;
	public float windResist;
	public float lightningResist;
	public PlayerData()
	{

	}
	public override float GetTableID()
	{
		return level;
	}
}
