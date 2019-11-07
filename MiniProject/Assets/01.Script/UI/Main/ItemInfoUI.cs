using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class ItemInfoUI : MonoBehaviour
{
	public Image itemImage;
	public Image frameImage;
	public Text itemNameText;
	public Text itemBaseInfoText;
	public Text skillInfoText;
	public void ShowItemlInfo(ItemData item)
	{
		itemImage.sprite = SpriteMng.Ins.itemAtlas.GetSprite(item.itemName);
		frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite(item.itemGradeType.ToString());
		itemNameText.text = item.itemName;
		itemBaseInfoText.text = string.Format("{0} {1}% 증가", Define.GetPartString(item.upgradeType), (int)(item.changeValue * 100));
		if (item.changeOption == eSkillOption.CoolTime)
		{
			skillInfoText.text = string.Format("{0} {1} {2}% 감소", JsonMng.Ins.playerSkillDataTable[item.changeSkill].skillName, item.changeOption.ToString(), (int)(item.changeSkillValue * 100));
		}
		else
		{
			skillInfoText.text = string.Format("{0} {1} {2}% 증가", JsonMng.Ins.playerSkillDataTable[item.changeSkill].skillName, item.changeOption.ToString(), (int)(item.changeSkillValue * 100));
		}
		gameObject.SetActive(true);

	}
}
