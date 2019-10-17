using UnityEngine;
using UnityEngine.UI;
public class SkillBuyUI : MonoBehaviour
{
	public Image skillImage;
	public Text skillName;
	public Text priceText;
	public void ShowSkillInfo(int skillID)
	{
		gameObject.SetActive(true);
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillID));
		skillName.text = "스킬 이름";
		priceText.text = "스킬 가격을 불러오자";
	}
}
