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
	public GameObject[] boxArr;
	public GameObject clickBox;
	public int selectSkillID;

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
		selectSkillID = 0;
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
		selectSkillID = sI;
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
		skillInfoUI.ShowSkillInfo(selectSkillID);
	}

	public void OnClickSkillBuyUI()
	{
		OffSelectBox();
		skillBuyUI.ShowSkillInfo(selectSkillID);
	}
	public void OnClickBuySkill()
	{
		JsonMng.Ins.playerInfoDataTable.haveSkillList.Add(selectSkillID);
		if (JsonMng.Ins.playerInfoDataTable.gold >= JsonMng.Ins.playerSkillDataTable[selectSkillID].price)
		{
			JsonMng.Ins.playerInfoDataTable.gold -= JsonMng.Ins.playerSkillDataTable[selectSkillID].price;
			shopUI.infinityScoll.BuySkill(selectSkillID);
			ReNew();
		}
		else
		{
			//TODO : 돈부족
		}
	}
	public void OnClickRemoveSkill()
	{
		JsonMng.Ins.playerInfoDataTable.RemoveSkill(selectSkillID);
		ReNew();
	}
	public void OnClickSetSkill()
	{
		OffSelectBox();
		clickBox.gameObject.SetActive(true);
		shopUI.ChangeSelectFlag(true);
	}
}
