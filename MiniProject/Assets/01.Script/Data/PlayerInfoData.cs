using System.Collections.Generic;

public class PlayerInfoData
{
	public int gold;
	public int diamond;
	public List<int> setSkillList = new List<int>();
	public List<int> haveSkillList = new List<int>();
	//TODO : 스테이지정보
	public PlayerInfoData()
	{
	}
	public void RemoveSkill(int skillID)
	{
		for (int i = 0; i < setSkillList.Count; ++i)
		{
			if (setSkillList[i] == skillID)
			{
				setSkillList[i] = 0;
				break;
			}
		}
	}
	public void SetSkill(int newSkillID,int setSkillListIndex)
	{
		if (newSkillID == setSkillList[setSkillListIndex]) return;
		else if (setSkillList.Contains(newSkillID))
		{
			RemoveSkill(newSkillID);
		}
		setSkillList[setSkillListIndex] = newSkillID;
	}
	public void AddGold(int _gold)
	{
		gold += _gold;
	}
	public int FindSkill(int skillID)
	{
		for (int i = 0; i < setSkillList.Count; ++i)
		{

			if(setSkillList[i] == skillID)
			{
				return i;
			}
		}
		return -1;
	}
}
