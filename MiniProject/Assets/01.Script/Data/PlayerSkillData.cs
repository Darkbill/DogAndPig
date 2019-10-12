using GlobalDefine;
public class PlayerSkillData : TableBase
{
	public float skillID;
	public eAttackType skillType;
	public float target;
	public float[] optionArr;
	public PlayerSkillData()
	{

	}
	public override float GetTableID()
	{
		return skillID;
	}
}
