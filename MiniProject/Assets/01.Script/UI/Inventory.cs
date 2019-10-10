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
	//TODO : 재사용 스크롤
	private void SetInventory()
	{
		for (int i = 0; i < itemList.Count; ++i)
		{
			inventorySlotArr[i].SetSlot(itemList[i].ItemCode.ToString());
		}
		for (int i = itemList.Count; i < inventorySlotArr.Length; ++i)
		{
			inventorySlotArr[i].SetSlot("None");
		}

	}
}
