using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
using System.Text;
using System.Collections.Generic;
public class InventoryUI : MonoBehaviour
{
	public ItemSlotUI[] equipSlotUIArr;
	public ItemSlotUI[] itemSlotUIArr;
	public ItemBoxUI itemBoxUI;
	public ItemInfoUI itemInfoUI;
	public Text goldText;
	public Text infoText;
	private void OnEnable()
	{
		UIMng.Ins.ReNew();
		Renew();
	}
	public void Renew()
	{
		goldText.text = JsonMng.Ins.playerInfoDataTable.gold.ToString();
		Setting();
		ShowInfo();
	}
	private void ShowInfo()
	{
		StringBuilder builder = new StringBuilder();
		
		for(int i = 0; i < equipSlotUIArr.Length; ++i)
		{
			if (equipSlotUIArr[i].item == null) continue;
			builder.Append(Define.GetItemBaseInfoText(equipSlotUIArr[i].item));
			builder.Append("\n");
		}

		//따로 표기하기위함!
		//효과가 겹치는 아이템 설명 합치기

		Dictionary<KeyValuePair<int, eSkillOption>, float> skillInfo = new Dictionary<KeyValuePair<int, eSkillOption>, float>();
		for (int i = 0; i < equipSlotUIArr.Length; ++i)
		{
			if (equipSlotUIArr[i].item == null) continue;
			int changeSkillID = equipSlotUIArr[i].item.changeSkill;
			eSkillOption changeSkillOption = equipSlotUIArr[i].item.changeOption;
			KeyValuePair<int, eSkillOption> cOption = new KeyValuePair<int, eSkillOption>(changeSkillID,changeSkillOption);
			if(skillInfo.ContainsKey(cOption))
			{
				skillInfo[cOption] += equipSlotUIArr[i].item.changeSkillValue;
			}
			else
			{
				skillInfo.Add(new KeyValuePair<int, eSkillOption>(changeSkillID, changeSkillOption), equipSlotUIArr[i].item.changeSkillValue);
			}
		}
		var e = skillInfo.GetEnumerator();
		while(e.MoveNext())
		{
			ItemData item = new ItemData();
			item.changeSkill = e.Current.Key.Key;
			item.changeOption = e.Current.Key.Value;
			item.changeSkillValue = e.Current.Value;
			builder.Append(Define.GetItemSkillInfoText(item));
			builder.Append("\n");
		}
		
		infoText.text = builder.ToString();
	}
	public void Setting()
	{
		var equip = JsonMng.Ins.playerInfoDataTable.equipItemList;
		for(int i = 0; i < equip.Length; ++i)
		{
			if (equip[i] == null) equipSlotUIArr[i].Setting();
			else equipSlotUIArr[i].Setting(equip[i]);
		}
		var inventory = JsonMng.Ins.playerInfoDataTable.haveItemList;
		for (int i = 0; i < inventory.Count; ++i)
		{
			itemSlotUIArr[i].Setting(inventory[i]);
		}
		for (int i = inventory.Count; i < itemSlotUIArr.Length; ++i)
		{
			itemSlotUIArr[i].Setting();
		}
	}
	public void CreateItem()
	{
		if (JsonMng.Ins.playerInfoDataTable.haveItemList.Count == Define.inventoryCount)
		{
			Debug.Log("슬롯부족");
			return;
		}
		if (JsonMng.Ins.playerInfoDataTable.gold >= 100)
		{
			JsonMng.Ins.playerInfoDataTable.gold -= 100;
			ItemData item = new ItemData();
			int itemID = Random.Range(1, 4);
			item = JsonMng.Ins.itemDataTable[itemID];
			item.itemGradeType = Define.GetGrade();
			item.changeValue = Define.GetChangeValue(item.itemGradeType);
			item.changeSkill = JsonMng.Ins.GetRandomSkillIndex();
			int[] optionArr = JsonMng.Ins.playerSkillDataTable[item.changeSkill].changeAbleOption;
			item.changeOption = (eSkillOption)optionArr[Random.Range(0, optionArr.Length - 1)];
			item.changeSkillValue = Random.Range(0.0f, 1.0f);
			itemBoxUI.SetItem(item);
		}
		else
		{
			//TODO : 돈부족
		}
	}
	public void ShowItemInfo(int selectID)
	{
		itemInfoUI.ShowItemlInfo(JsonMng.Ins.playerInfoDataTable.haveItemList[selectID]);
	}
	public void ShowEquipItemInfo(int selectID)
	{
		itemInfoUI.ShowItemlInfo(JsonMng.Ins.playerInfoDataTable.equipItemList[selectID]);
	}
	public void RemoveItem(int selectID)
	{
		JsonMng.Ins.playerInfoDataTable.RemoveItem(selectID);
		Renew();
	}
	public void EquipItem(int selectID)
	{
		JsonMng.Ins.playerInfoDataTable.EquipItem(selectID);
		Renew();
	}
	public void TakeOffItem(int selectID)
	{
		JsonMng.Ins.playerInfoDataTable.TakeOffItem(selectID);
		Renew();
	}
}
