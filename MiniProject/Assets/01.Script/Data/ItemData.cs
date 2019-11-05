using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : TableBase
{
	public int itemID;
	public string itemName;
	public override int GetTableID()
	{
		return itemID;
	}
}
