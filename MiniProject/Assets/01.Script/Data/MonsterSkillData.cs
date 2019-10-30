using GlobalDefine;
public class MonsterSkillData : TableBase
{
	public float skillID;
	public string skillName;
	public string decal;
	public eAttackType skillType;
	public eSkillType target;
	public int[] optionArr;
	public override float GetTableID()
	{
		return skillID;
	}
	public MonsterSkillData()
	{

	}
}