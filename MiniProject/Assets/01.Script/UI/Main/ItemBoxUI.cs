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
		itemBaseInfo.text = Define.GetItemBaseInfoText(item);
		itemSkillInfo.text = Define.GetItemSkillInfoText(item);
	
		gameObject.SetActive(true);
	}
	
	public void GetItem()
	{
		JsonMng.Ins.playerInfoDataTable.haveItemList.Add(item.Copy());
		UIMng.Ins.ReNew();
		UIMng.Ins.inventoryUI.Renew();
	}
}