using GlobalDefine;
using System.Collections.Generic;
public class PlayerSkillData : TableBase
{
	public float skillID;
	public eAttackType skillType;
	public eSkillType target;
	public float[] optionArr;
	public PlayerSkillData()
	{

	}
	public override float GetTableID()
	{
		return skillID;
	}
}
