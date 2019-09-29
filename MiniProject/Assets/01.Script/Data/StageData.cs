using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class StageData : TableBase
{
	public int monsterCount;
	public float limitTime;
	public eSpawnWay spawnWayType;
	public int[] monsterIDArr;
	public StageData(int i,int c,float l,int w,int[] ml)
	{
		tableID = i;
		monsterCount = c;
		limitTime = l;
		spawnWayType = (eSpawnWay)w;
		monsterIDArr = ml;
	}
	public StageData()
	{

	}
}
