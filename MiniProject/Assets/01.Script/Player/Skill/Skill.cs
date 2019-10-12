using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public abstract class Skill : MonoBehaviour
{
	public int skillID;
	public string skillName;
	protected eAttackType skillType;
	protected eSkillType target;
	abstract public void SkillSetting();
}
