using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillData : TableBase
{
	public float skillID;
	public string skillName;
	public float coolTime;
	public PlayerSkillData()
	{

	}
	public PlayerSkillData(int sI,string sN,int cT)
	{
		skillID = sI;
		skillName = sN;
		coolTime = cT;
	}
	public override float GetTableID()
	{
		return skillID;
	}
}
