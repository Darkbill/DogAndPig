using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillData : TableBase
{
	public int skillID;
	public string skillName;
	public int coolTime;
	public PlayerSkillData()
	{

	}
	public PlayerSkillData(int sI,string sN,int cT)
	{
		skillID = sI;
		skillName = sN;
		coolTime = cT;
	}
	public override int GetTableID()
	{
		return skillID;
	}
}
