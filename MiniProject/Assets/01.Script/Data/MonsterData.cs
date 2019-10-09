using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : TableBase
{
	public int monsterID;
	public string monsterName;
	public int size;
	public int moveType;
	public int moveSpeed;
	public int rotationSpeed;
	public int attackType;
	public int attackSpeed;
	public int attackRange;
	public int attackAngle;
	public int healthPoint;
	public int damage;
	public int armor;
	public int skillIndex;

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
	public override int GetTableID()
	{
		return monsterID;
	}
}
