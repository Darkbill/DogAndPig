using System.Collections.Generic;
public class PlayerSkillTextData : TableBase
{
	public int skillID;
	public string skillName;
	public string damage;
	public string coolTime;
	public string activeTime;
	public string speed;
	public string spawnDelay;
	public string spawnActiveTime;
	public string buffActivePer;
	public string buffEndTime;
	public string buffChangeValue;
	public List<string> optionTextList = new List<string>();
	public override int GetTableID()
	{
		optionTextList.Add(damage);
		optionTextList.Add(coolTime);
		optionTextList.Add(activeTime);
		optionTextList.Add(speed);
		optionTextList.Add(spawnDelay);
		optionTextList.Add(spawnActiveTime);
		optionTextList.Add(buffActivePer);
		optionTextList.Add(buffEndTime);
		optionTextList.Add(buffChangeValue);

		return skillID;
	}
}
