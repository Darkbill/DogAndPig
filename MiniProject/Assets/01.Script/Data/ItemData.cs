using GlobalDefine;

public class ItemData : TableBase
{
	public int itemID;
	public string itemName;
	public eItemType itemType;
	public eItemGradeType itemGradeType;
	public eUpgradeType upgradeType;
	public float changeValue;
	public int changeSkill;
	public eSkillOption changeOption;
	public float changeSkillValue;
	public override int GetTableID()
	{
		return itemID;
	}
	public ItemData()
	{

	}
}
