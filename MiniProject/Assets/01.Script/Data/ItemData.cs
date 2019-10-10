using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : TableBase
{
	public float itemID;
	public string itemName;
	public override float GetTableID()
	{
		return itemID;
	}
}
