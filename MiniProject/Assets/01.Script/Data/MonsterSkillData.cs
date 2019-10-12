using GlobalDefine;
public class MonsterSkillData : TableBase
{
	public float skillID;
	public eAttackType skillType;
	public float target;
	public float[] optionArr;
	public override float GetTableID()
	{
		return skillID;
	}
	public MonsterSkillData()
	{

	}
}
