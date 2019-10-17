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
	public int skillID;
	public void Setting(int sI)
	{
		gameObject.SetActive(true);
		skillID = sI;
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillID));
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
			skillText.text = "SkillName";
			priceText.text = "";
		}	
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