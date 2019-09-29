using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
	public Image slotImgae;
	public void SetSlot(string spriteID)
	{
		slotImgae.sprite = SpriteMng.Ins.itemAtlas.GetSprite(spriteID);
	}
	public void OnClickInventorySlot()
	{

	}
}
