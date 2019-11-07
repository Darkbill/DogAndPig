using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class ItemBoxUI : MonoBehaviour
{
	private ItemData item;
	public Image itemImage;
	public Image frameImage;
	public Text itemNameText;
	public Text itemBaseInfo;
	public Text itemSkillInfo;
	public void SetItem(ItemData _item)
	{
		item = _item;
		itemImage.sprite = SpriteMng.Ins.itemAtlas.GetSprite(item.itemName);
		frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite(item.itemGradeType.ToString());
		itemNameText.text = item.itemName;
		itemBaseInfo.text = string.Format("{0} {1}% 증가", Define.GetPartString(item.upgradeType), (int)(item.changeValue * 100));
		if (item.changeOption == eSkillOption.CoolTime)
		{
			itemSkillInfo.text = string.Format("{0} {1} {2}% 감소", JsonMng.Ins.playerSkillDataTable[item.changeSkill].skillName, item.changeOption.ToString(), (int)(item.changeSkillValue * 100));
		}
		else
		{
			itemSkillInfo.text = string.Format("{0} {1} {2}% 증가", JsonMng.Ins.playerSkillDataTable[item.changeSkill].skillName, item.changeOption.ToString(), (int)(item.changeSkillValue * 100));
		}
	
		gameObject.SetActive(true);
	}
	
	public void GetItem()
	{
		JsonMng.Ins.playerInfoDataTable.haveItemList.Add(item.Copy());
		UIMng.Ins.ReNew();
		UIMng.Ins.inventoryUI.Renew();
	}
}