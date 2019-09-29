using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : TableBase
{
	public int monsterID;
	public string monsterName;
	public int monsterHP;
	public double moveSpeed;
	public MonsterData(int i,string n,int h, double s)
	{
		tableID = i;
		monsterID = i;
		monsterName = n;
		monsterHP = h;
		moveSpeed = s;
	}
	public MonsterData()
	{

	}
}
