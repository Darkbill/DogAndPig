using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataTable : TableBase
{
	public int stageID;
	public int stageLevel;
	public int enemyIndex;
	public int enemyLevel;
	public float enemyPosX;
	public float enemyPosY;
	public int boss;

	public override float GetTableID()
	{
		return stageID;
	}
}
