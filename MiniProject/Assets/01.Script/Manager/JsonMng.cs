using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System;

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
	public Dictionary<int, PlayerData> playerDataTable { get; private set; } = new Dictionary<int, PlayerData>();
	public Dictionary<int, PlayerSkillData> playerSkillDataTable { get; private set; } = new Dictionary<int, PlayerSkillData>();
	public Dictionary<int, MonsterData> monsterDataTable { get; private set; } = new Dictionary<int, MonsterData>();
	public Dictionary<int, MonsterSkillData> monsterSkillDataTable { get; private set; } = new Dictionary<int, MonsterSkillData>();
	public Dictionary<int, StageDataTable> stageDataTable { get; private set; } = new Dictionary<int, StageDataTable>();
	public Dictionary<int, EXPData> expDataTable { get; private set; } = new Dictionary<int, EXPData>();
	public PlayerInfoData playerInfoDataTable { get; private set; } = new PlayerInfoData();
	//테스트 코드
	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadAll();
	}
	private void LoadAll()
	{
		LoadPlayerInfoData();
		LoadPlayerData();
		LoadPlayerSkillData();
		LoadMonsterData();
		LoadMonsterSkillData();
		LoadExpDataTable();
		LoadStageData();
	}

	private void LoadPlayerInfoData()
	{
#if UNITY_EDITOR_WIN
		string JsonString = File.ReadAllText(string.Format("{0}/Resources/LitJson/{1}.json", Application.dataPath, "PlayerInfoDataTable"));
#else
		string JsonString = File.ReadAllText(string.Format("{0}/Resources/LitJson/{1}.json", Application.persistentDataPath, fileName));
#endif
		JsonData jsonData = JsonMapper.ToObject(JsonString);
		playerInfoDataTable = JsonMapper.ToObject<PlayerInfoData>(jsonData.ToJson());
	
	}
	private void LoadExpDataTable()
	{
		LoadData("EXPDataTable", expDataTable);
	}
	private void LoadStageData()
	{
		LoadData("StageDataTable", stageDataTable);
	}
	private void LoadPlayerData()
	{
		LoadData("PlayerDataTable", playerDataTable);
	}
	private void LoadMonsterData()
	{
		LoadData("MonsterDataTable", monsterDataTable);
	}
	private void LoadPlayerSkillData()
	{
		LoadData("PlayerSkillDataTable", playerSkillDataTable);
	}
	private void LoadMonsterSkillData()
	{
		LoadData("MonsterSkillDataTable", monsterSkillDataTable);
	}
	private void LoadData<T>(string fileName,Dictionary<int,T> table) where T : TableBase
	{
#if UNITY_EDITOR_WIN
		string JsonString = File.ReadAllText(string.Format("{0}/Resources/LitJson/{1}.json", Application.dataPath, fileName));
#else
		string JsonString = File.ReadAllText(string.Format("{0}/Resources/LitJson/{1}.json", Application.persistentDataPath, fileName));
#endif
		JsonData jsonData = JsonMapper.ToObject(JsonString);
		for (int i = 0; i < jsonData.Count; ++i)
		{
			T save = JsonMapper.ToObject<T>(jsonData[i].ToJson());
			table.Add((int)save.GetTableID(), save);
		}
	}

}
