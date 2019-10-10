using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : TableBase
{
	public float itemID;
	public string itemName;
	public ItemData(int i, string n)
	{
		itemID = i;
		itemName = n;
	}
	public override float GetTableID()
	{
		return itemID;
	}
}
