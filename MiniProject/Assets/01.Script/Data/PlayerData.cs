using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : TableBase
{
	public int level;
	public int healthPoint;
	public int damage;
	public int moveSpeed;
	public int criticalChange;
	public int criticalDamage;
	public int armor;
	public PlayerData()
	{

	}
	public PlayerData(int l,int h,int d,int mS,int cC,int cD,int a)
	{
		level = l;
		healthPoint = h;
		damage = d;
		moveSpeed = mS;
		criticalChange = cC;
		criticalDamage = cD;
		armor = a;
	}
	public override int GetTableID()
	{
		return level;
	}
}
