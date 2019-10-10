﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillData : TableBase
{
	public float skillID;
	public string skillName;
	public float[] optionArr;
	public PlayerSkillData()
	{

	}
	public override float GetTableID()
	{
		return skillID;
	}
}
