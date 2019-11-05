using GlobalDefine;
public class MonsterSkillData : TableBase
{
	public int skillID;
	public string skillName;
	public string decal;
	public eAttackType skillType;
	public eSkillType target;
	public int[] optionArr;
	public override int GetTableID()
	{
		return skillID;
	}
	public MonsterSkillData()
	{

	}
}