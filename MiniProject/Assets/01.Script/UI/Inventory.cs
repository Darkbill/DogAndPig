using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


struct Itemtype
{
	public int ItemCode;
	public string ItemLink;

	public Itemtype(int code, string link)
	{
		ItemCode = code;
		ItemLink = link;
	}
}
public class Inventory : MonoBehaviour
{
	// * TestCode * //
	private List<Itemtype> itemList = new List<Itemtype>();
	private void Awake()
	{
		Setting();
	}

	private void Setting()
	{
		Itemtype item = new Itemtype(0, "coin");
		itemList.Add(item);
		item = new Itemtype(1, "example");
		itemList.Add(item);
		SetInventory();
	}
	// * TestCode * //
	public InventorySlot[] inventorySlotArr;
	//TODO : 서버에서 유저 인벤토리 정보 받아와서 인벤토리 보여주기
	//TODO : 재사용 스크롤
	private void SetInventory()
	{
		for (int i = 0; i < itemList.Count; ++i)
		{
			inventorySlotArr[i].SetSlot(itemList[i].ItemCode.ToString());
			//TODO : ID는 계산이 필요한게 아니니 전부 String이 맞음
		}
		for (int i = itemList.Count; i < inventorySlotArr.Length; ++i)
		{
			inventorySlotArr[i].SetSlot("None");
		}
		//Transform but = gameObject.transform.GetChild(0);
		//RectTransform rowRectTransform = but.GetComponent<RectTransform>();
		//RectTransform cotainerRectTransform = gameObject.GetComponent<RectTransform>();

		//float width = cotainerRectTransform.rect.width;
		//float ratio = width / rowRectTransform.rect.width;
		//float height = rowRectTransform.rect.height;
		//float ScroolHeight = height * SlotSiz / 3;

		//for (int i = 0; i < inventorySlotArr.Length; ++i)
		//{

		//	if (itemList.Count > i)
		//	{
		//		inventorySlotArr = UIMng.Ins.ItemInventory.transform.GetChild(i);
		//		inventorySlotArr.GetComponent<Image>().sprite = Resources.Load<Sprite>(ItemList[i].ItemLink);

		//	}
		//	else
		//	{
		//		but = UIMng.Ins.ItemInventory.transform.GetChild(i);
		//		but.GetComponent<Image>().sprite = Resources.Load<Sprite>("pig");
		//	}
		//}
	}
}
