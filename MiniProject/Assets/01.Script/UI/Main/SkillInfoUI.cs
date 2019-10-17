using UnityEngine;
using UnityEngine.UI;

public class SkillInfoUI : MonoBehaviour
{
	public Image skillImage;
	public Text skillName;
	public Text skillInfoText;
	public void ShowSkillInfo(int skillID)
	{
		gameObject.SetActive(true);
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillID));
		skillName.text = "스킬 이름";
		skillInfoText.text = "스킬 설명을 불러오자";
	}
}
