using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class SkillSlotUI : MonoBehaviour
{
	public Image skillImage;
	public Image frameImage;
	public Text skillName;
	public bool changeFlag;
	private int skillID;
	public int slotIndex;
	
	public void Setting(int sI)
	{
		skillID = sI;
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillID.ToString()));
		if (skillID == 0)
		{
			skillName.text = "";
		}
		else
		{
			skillName.text = JsonMng.Ins.playerSkillDataTable[skillID].skillName;
		}
		if (sI == 0)
		{
			frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite("None");
			return;
		}
		frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite(
			JsonMng.Ins.playerSkillDataTable[sI].activeType.ToString());
	}
	public void OnClickSetSkill()
	{
		if (changeFlag)
		{
			int sI = UIMng.Ins.selectID;
			int findIndex = JsonMng.Ins.playerInfoDataTable.FindSkill(sI);
			if(findIndex != -1 && skillID != 0)
			{
				JsonMng.Ins.playerInfoDataTable.SetSkill(skillID, findIndex);
			}
			JsonMng.Ins.playerInfoDataTable.SetSkill(sI,slotIndex);
			UIMng.Ins.ReNew();
		}
		else
		{
			if (skillID == 0) return;
			UIMng.Ins.OnClickSkill(skillID, eBoxType.Remove);
		}
	}
}
