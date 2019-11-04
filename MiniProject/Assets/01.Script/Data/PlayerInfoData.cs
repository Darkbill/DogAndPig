using System.Collections.Generic;
using GlobalDefine;
public class PlayerInfoData
{
	public int gold;
	public int diamond;
	public List<int> setSkillList = new List<int>();
	public List<int> haveSkillList = new List<int>();
	public int stageLevel;
	public int playerLevel;
	public int exp;
	public int startCount;
	public int adCount;
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
	public bool StartLobby()
	{
		startCount++;
		if(startCount == Define.lobbyStartCount)
		{
			startCount = 0;
			return true;
		}
		return false;
	}
	public void GameOver()
	{
		if (adCount == Define.adCount)
		{
			return;
		}
		adCount++;
	}
}
