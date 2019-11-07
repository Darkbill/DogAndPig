using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
using System.Text;
public class InventoryUI : MonoBehaviour
{
	public EquipSlotUI[] equipSlotUIArr;
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
		//TODO : 중복효과 텍스트 합치기
		for(int i = 0; i < equipSlotUIArr.Length; ++i)
		{
			if (equipSlotUIArr[i].item == null) continue;
			builder.Append(string.Format("{0} {1}% 증가 \n", Define.GetPartString(equipSlotUIArr[i].item.upgradeType), (int)(equipSlotUIArr[i].item.changeValue * 100)));
		}
		builder.Append("\n");
		for (int i = 0; i < equipSlotUIArr.Length; ++i)
		{
			if (equipSlotUIArr[i].item == null) continue;
			if (equipSlotUIArr[i].item.changeOption == eSkillOption.CoolTime)
			{
				builder.Append(string.Format("{0} {1} {2}% 감소", JsonMng.Ins.playerSkillDataTable[equipSlotUIArr[i].item.changeSkill].skillName, equipSlotUIArr[i].item.changeOption.ToString(), (int)(equipSlotUIArr[i].item.changeSkillValue * 100)));
			}
			else
			{
				builder.Append(string.Format("{0} {1} {2}% 증가", JsonMng.Ins.playerSkillDataTable[equipSlotUIArr[i].item.changeSkill].skillName, equipSlotUIArr[i].item.changeOption.ToString(), (int)(equipSlotUIArr[i].item.changeSkillValue * 100)));
			}
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
