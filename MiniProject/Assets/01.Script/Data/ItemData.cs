using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : TableBase
{
	public int itemID;
	public string itemName;
	public ItemData(int i, string n)
	{
		itemID = i;
		itemName = n;
	}
	public override int GetTableID()
	{
		return itemID;
	}
}
