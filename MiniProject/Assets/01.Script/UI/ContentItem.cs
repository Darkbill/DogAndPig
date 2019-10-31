using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class ContentItem : MonoBehaviour
{
	public Image skillImage;
	public Text skillText;
	public Text priceText;
	public Image lockImage;
	public Image goldImage;
	public Image frameImage;
	public int skillID;
	public void Setting(int sI)
	{
		gameObject.SetActive(true);
		skillID = sI;
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillID));
		skillText.text = JsonMng.Ins.playerSkillDataTable[skillID].skillName;
		
		if (JsonMng.Ins.playerInfoDataTable.haveSkillList.Contains(skillID) == true)
		{
			lockImage.gameObject.SetActive(false);
			goldImage.gameObject.SetActive(false);
			priceText.gameObject.SetActive(false);
		}
		else
		{
			lockImage.gameObject.SetActive(true);
			goldImage.gameObject.SetActive(true);
			priceText.gameObject.SetActive(true);
			lockImage.color = new Color(1, 1, 1, 0.75f);
			priceText.text = JsonMng.Ins.playerSkillDataTable[skillID].price.ToString();
		}	
		if(sI == 0)
		{
			frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite("None");
			return;
		}
		frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite(
			JsonMng.Ins.playerSkillDataTable[sI].activeType.ToString());
	}
	public void OnClickHaveSkill()
	{
		UIMng.Ins.OnClickSkill(skillID,eBoxType.Set);
	}
	public void OnClickBuySkill()
	{
		UIMng.Ins.OnClickSkill(skillID,eBoxType.Buy);
	}
}