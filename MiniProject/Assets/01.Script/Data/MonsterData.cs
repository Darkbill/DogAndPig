using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : TableBase
{
	public float monsterID;
	public string monsterName;
	public float size;
	public float moveType;
	public float moveSpeed;
	public float rotationSpeed;
	public float attackType;
	public float attackSpeed;
	public float attackRange;
	public float attackAngle;
	public float healthPoint;
	public float damage;
	public float armor;
	public float skillIndex;

	public MonsterData(int i,string n,int s, int mT,int mS,int rS,int aT, int aS,int aR, int aA,int hP,int d,int a,int sI)
	{
		monsterID = i;
		monsterName = n;
		size = s;
		moveType = mT;
		moveSpeed = mS;
		rotationSpeed = rS;
		attackType = aT;
		attackSpeed = aS;
		attackRange = aR;
		attackAngle = aA;
		healthPoint = hP;
		damage = d;
		armor = a;
		skillIndex = sI;
	}
	public MonsterData()
	{

	}
	public override float GetTableID()
	{
		return (float)monsterID;
	}
}
