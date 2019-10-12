using GlobalDefine;
public class MonsterSkillData : TableBase
{
	public float skillID;
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
