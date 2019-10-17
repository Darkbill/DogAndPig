using GlobalDefine;
using System.Collections.Generic;
public class PlayerSkillData : TableBase
{
	public int skillID;
	public int price;
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
