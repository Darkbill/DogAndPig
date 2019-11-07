using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class UIMng : MonoBehaviour
{
	#region SINGLETON
	static UIMng _instance = null;



	public static UIMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UIMng)) as UIMng;
				if (_instance == null)
				{
					_instance = new GameObject("UIMng", typeof(UIMng)).GetComponent<UIMng>();
				}
			}

			return _instance;
		}
	}
	#endregion

	public ShopUI shopUI;
	public Text goldText;
	public Text diamondText;
	public SkillInfoUI skillInfoUI;
	public SkillBuyUI skillBuyUI;
	public InventoryUI inventoryUI;

	public GameObject[] boxArr;
	public GameObject clickBox;
	public int selectID;

	/* Setting */
	private void Start()
	{
		if(JsonMng.Ins.IsDone == false)
		{
			JsonMng.Ins.LoadAll();
		}
		else Setting();
	}
	public void Setting()
	{
		shopUI.Setting();
		SetBaseUI();
	}
	private void SetBaseUI()
	{
		goldText.text = JsonMng.Ins.playerInfoDataTable.gold.ToString();
		diamondText.text = JsonMng.Ins.playerInfoDataTable.diamond.ToString();
	}
	public void ReNew()
	{
		selectID = 0;
		clickBox.gameObject.SetActive(false);
		OffSelectBox();
		shopUI.ReNew();
		SetBaseUI();
	}

	/* 버튼호출 */
	public void ClickGameStart()
	{
		if(JsonMng.Ins.playerInfoDataTable.StartLobby())
		{
			Debug.Log("광고");
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}
	public void OnClickSkill(int sI,eBoxType type)
	{
		if(clickBox.gameObject.activeSelf)
		{
			clickBox.gameObject.SetActive(false);
			shopUI.ChangeSelectFlag(false);
		}
		selectID = sI;
		for (int i = 0; i < boxArr.Length; ++i)
		{
			if (i == (int)type)
			{
				boxArr[i].gameObject.SetActive(true);
				boxArr[i].gameObject.transform.position = Input.mousePosition;
			}
			else boxArr[i].gameObject.SetActive(false);
		}
	}
	public void onClickItem(int sI, eBoxType type)
	{
		selectID = sI;
		for (int i = 0; i < boxArr.Length; ++i)
		{
			if (i == (int)type)
			{
				boxArr[i].gameObject.SetActive(true);
				boxArr[i].gameObject.transform.position = Input.mousePosition;
			}
			else boxArr[i].gameObject.SetActive(false);
		}
	}
	public void OffSelectBox()
	{
		for (int i = 0; i < boxArr.Length; ++i)
		{
			boxArr[i].gameObject.SetActive(false);
		}
	}
	public void OnClickSkillInfo()
	{
		OffSelectBox();
		skillInfoUI.ShowSkillInfo(selectID);
	}

	public void OnClickSkillBuyUI()
	{
		OffSelectBox();
		skillBuyUI.ShowSkillInfo(selectID);
	}
	public void OnClickBuySkill()
	{
		JsonMng.Ins.playerInfoDataTable.haveSkillList.Add(selectID);
		if (JsonMng.Ins.playerInfoDataTable.gold >= JsonMng.Ins.playerSkillDataTable[selectID].price)
		{
			JsonMng.Ins.playerInfoDataTable.gold -= JsonMng.Ins.playerSkillDataTable[selectID].price;
			shopUI.infinityScoll.BuySkill(selectID);
			ReNew();
		}
		else
		{
			//TODO : 돈부족
		}
	}
	public void OnClickRemoveSkill()
	{
		JsonMng.Ins.playerInfoDataTable.RemoveSkill(selectID);
		ReNew();
	}
	public void OnClickSetSkill()
	{
		OffSelectBox();
		clickBox.gameObject.SetActive(true);
		shopUI.ChangeSelectFlag(true);
	}
	public void OnClickItemInfo()
	{
		OffSelectBox();
		inventoryUI.ShowItemInfo(selectID);
	}
	public void OnClickItemRemove()
	{
		OffSelectBox();
		inventoryUI.RemoveItem(selectID);
	}
	public void OnClickEquipItem()
	{
		OffSelectBox();
		inventoryUI.EquipItem(selectID);
	}
	public void OnClickTakeOffItem()
	{
		OffSelectBox();
		inventoryUI.TakeOffItem(selectID);
	}
	public void OnClickEquipItemInfo()
	{
		OffSelectBox();
		inventoryUI.ShowEquipItemInfo(selectID);
	}
}
