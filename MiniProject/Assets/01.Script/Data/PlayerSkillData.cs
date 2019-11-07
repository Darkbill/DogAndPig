using GlobalDefine;
public class PlayerSkillData : TableBase
{
	public int skillID;
	public string skillName;
	public eSkillActiveType activeType;
	public int price;
	public eAttackType skillType;
	public eSkillType target;
	public float[] optionArr;
	public int[] changeAbleOption;
	public PlayerSkillData()
	{

	}
	public override int GetTableID()
	{
		return skillID;
	}
}
