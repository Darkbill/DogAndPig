using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillData : TableBase
{
	public float skillID;
	public string skillName;
	public float[] optionArr;
	public override float GetTableID()
	{
		return skillID;
	}
	public MonsterSkillData()
	{

	}
}
