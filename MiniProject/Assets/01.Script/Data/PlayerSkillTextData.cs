using System.Collections.Generic;
using System.Text;
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
		byte[] bytesForEncoding = Encoding.UTF8.GetBytes(damage);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(coolTime);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(activeTime);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(speed);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(spawnDelay);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(spawnActiveTime);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(buffActivePer);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(buffEndTime);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		bytesForEncoding = Encoding.UTF8.GetBytes(buffChangeValue);
		optionTextList.Add(Encoding.UTF8.GetString(bytesForEncoding));
		return skillID;
	}
}
