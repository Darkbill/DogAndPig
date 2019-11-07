using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class ItemSlotUI : MonoBehaviour
{
	public enum eItemSlotType
	{
		Equip,
		Have,
	}
	public eItemSlotType itemSlotType;
	public ItemData item;
	public Image frameImage;
	public Image itemImage;
	public int slotIndex;
	public void Setting(ItemData _item = null)
	{
		item = _item;
		if (item == null)
		{
			item = null;
			frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite("None");
			itemImage.sprite = SpriteMng.Ins.itemAtlas.GetSprite("None");
		}
		else
		{
			frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite(item.itemGradeType.ToString());
			itemImage.sprite = SpriteMng.Ins.itemAtlas.GetSprite(item.itemName);
		}
	}
	public void OnClickItem()
	{
		if (item == null) return;

		if (itemSlotType == eItemSlotType.Have)
		{
			UIMng.Ins.onClickItem(slotIndex, eBoxType.Equip);
		}
		else UIMng.Ins.onClickItem(slotIndex, eBoxType.EquitTakeOff);
	}
}
