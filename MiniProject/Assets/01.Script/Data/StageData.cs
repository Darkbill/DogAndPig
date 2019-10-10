using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class StageData : TableBase
{
	public float stageID;
	public float monsterCount;
	public float limitTime;
	public eSpawnWay spawnWayType;
	public float[] monsterIDArr;
	public StageData(int i,int c,float l,int w, float[] ml)
	{
		stageID = i;
		monsterCount = c;
		limitTime = l;
		spawnWayType = (eSpawnWay)w;
		monsterIDArr = ml;
	}
	public StageData()
	{

	}
	public override float GetTableID()
	{
		return stageID;
	}
}
