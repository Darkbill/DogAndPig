using UnityEngine;
using UnityEngine.UI;
public class SkillSlotUI : MonoBehaviour
{
	public Image skillImage;
	public void Setting(int skillID)
	{
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}",skillID.ToString()));
	}
}
