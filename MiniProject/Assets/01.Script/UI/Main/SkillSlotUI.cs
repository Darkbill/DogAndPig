using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class SkillSlotUI : MonoBehaviour
{
	public Image skillImage;
	public bool changeFlag;
	private int skillID;
	public int slotIndex;
	public void Setting(int sI)
	{
		skillID = sI;
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}",skillID.ToString()));
	}
	public void OnClickSetSkill()
	{
		if (changeFlag)
		{
			int sI = UIMng.Ins.selectSkillID;
			JsonMng.Ins.playerInfoDataTable.SetSkill(sI,slotIndex);
			Setting(sI);
			UIMng.Ins.shopUI.ReNew();
		}
		else
		{
			if (skillID == 0) return;
			UIMng.Ins.OnClickSkill(skillID, eBoxType.Remove);
		}
	}
}
