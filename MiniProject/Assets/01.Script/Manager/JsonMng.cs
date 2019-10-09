using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
public class JsonMng : MonoBehaviour
{
	#region SINGLETON
	static JsonMng _instance = null;

	public static JsonMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(JsonMng)) as JsonMng;
				if (_instance == null)
				{
					_instance = new GameObject("JsonMng", typeof(JsonMng)).GetComponent<JsonMng>();
				}
			}

			return _instance;
		}
	}
	#endregion
	public Dictionary<int, MonsterData> monsterDataTable { get; private set; } = new Dictionary<int, MonsterData>();
	public Dictionary<int, StageData> stageDataTable { get; private set; } = new Dictionary<int, StageData>();
	public Dictionary<int, PlayerData> playerDataTable { get; private set; } = new Dictionary<int, PlayerData>(); 
	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadAll();
	}
	private void LoadAll()
	{
		LoadMonsterData();
		LoadStageData();
		LoadPlayerData();
	}
	public void LoadPlayerData()
	{
		LoadData("PlayerDataTable", playerDataTable);
	}
	public void LoadMonsterData()
	{
		LoadData("MonsterDataTable", monsterDataTable);
	}
	public void LoadStageData()
	{
		LoadData("StageDataTable", stageDataTable);
	}
	public void LoadData<T>(string fileName,Dictionary<int,T> table) where T : TableBase
	{
		string JsonString = File.ReadAllText(string.Format("{0}/Resources/LitJson/{1}.json", Application.dataPath, fileName));
		JsonData jsonData = JsonMapper.ToObject(JsonString);
		for (int i = 0; i < jsonData.Count; ++i)
		{
			T save = JsonMapper.ToObject<T>(jsonData[i].ToJson());
			table.Add(save.GetTableID(), save);
		}
	}
}
