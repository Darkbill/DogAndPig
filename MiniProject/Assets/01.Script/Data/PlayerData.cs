using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : TableBase
{
	public float level;
	public float healthPoint;
	public float damage;
	public float moveSpeed;
	public float criticalChange;
	public float criticalDamage;
	public float armor;
	public PlayerData()
	{

	}
	public PlayerData(float l, float h, float d, float mS, float cC, float cD, float a)
	{
		level = l;
		healthPoint = h;
		damage = d;
		moveSpeed = mS;
		criticalChange = cC;
		criticalDamage = cD;
		armor = a;
	}
	public override float GetTableID()
	{
		return level;
	}
}
