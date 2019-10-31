using UnityEngine;
using UnityEngine.UI;

public class SkillIconInGame : MonoBehaviour
{
	public Image skillImage;
	public Image skillBG;
	public Image frameImage;
	public void Setting(int skillIndex)
	{
		Sprite sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillIndex));
		skillImage.sprite = sprite;
		skillBG.sprite = sprite;
		if (skillIndex != 0)
		{
			frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite(
				JsonMng.Ins.playerSkillDataTable[skillIndex].activeType.ToString());
		}
		else
		{
			frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite("None");
		}
	}
	public void ChangeFill(float fillValue)
	{
		skillImage.fillAmount = fillValue;
	}
}
