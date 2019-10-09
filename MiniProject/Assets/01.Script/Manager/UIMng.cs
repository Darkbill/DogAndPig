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
	public Inventory ItemInventory;
	public GameObject MainLobby;
	public GameObject UserInfomation;
	public GameObject Store;
	public GameObject Ranking;
	public GameObject Option;

	List<GameObject> ItemList = new List<GameObject>();

	void Update()
	{

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
