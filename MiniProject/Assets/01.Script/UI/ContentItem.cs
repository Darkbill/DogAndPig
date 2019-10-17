using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContentItem : MonoBehaviour
{
	public Image skillImage;
	public Text skillText;
	public Text priceText;
	public Image lockImage;
	public Image goldImage;
	public void Setting(int skillID)
	{
		gameObject.SetActive(true);
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
			priceText.gameObject.transform.parent.name = "";
		}	
	}
	public void OnClickHaveSkill()
	{
		//OnClickSkill(true);
	}
	public void OnClickBuySkill()
	{

	}
}