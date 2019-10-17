using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPData : TableBase
{
	public int level;
	public int requiredExp;
	public int cumulativeExp;
	public override float GetTableID()
	{
		return level;
	}
}
