using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	private void Start()
	{
		//TODO : 로딩 후 실행
		shopUI.Setting();
		goldText.text = JsonMng.Ins.playerInfoDataTable.gold.ToString();
		diamondText.text = JsonMng.Ins.playerInfoDataTable.diamond.ToString();
	}

	public void ClickGameStart()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}
	public void ClickGameOption()
	{

	}
	public void ClickGameStore()
	{

	}
	public void ClickGameRanking()
	{

	}
	public void ClickUserInfomation()
	{

	}

	public void ClickBack()
	{

	}
}
