using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Text;

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
	public Dictionary<int, PlayerSkillData> playerSkillDataTable { get; private set; } = new Dictionary<int, PlayerSkillData>();
	public Dictionary<int, MonsterData> monsterDataTable { get; private set; } = new Dictionary<int, MonsterData>();
	public Dictionary<int, MonsterSkillData> monsterSkillDataTable { get; private set; } = new Dictionary<int, MonsterSkillData>();
	public Dictionary<int, List<StageDataTable>> stageDataTable { get; private set; }
												= new Dictionary<int, List<StageDataTable>>();
	public Dictionary<int, EXPData> expDataTable { get; private set; } = new Dictionary<int, EXPData>();
	public Dictionary<int, ItemData> itemDataTable { get; private set; } = new Dictionary<int, ItemData>();
	public PlayerInfoData playerInfoDataTable { get; private set; } = new PlayerInfoData();
	public PlayerData playerDataTable { get; private set; } = new PlayerData();
	public Dictionary<int, PlayerSkillTextData> playerSkillTextDataTable { get; private set; } = new Dictionary<int, PlayerSkillTextData>();
	private const int AllDownCount = 9;
	private int cDownCount = 0;
	public bool IsDone
	{
		get
		{
			if (AllDownCount == cDownCount) return true;
			else return false;
		}
	}
	//테스트 코드
	private void Awake()
	{
		DontDestroyOnLoad(this);
	}
	public void LoadAll()
	{
		LoadPlayerData();
		LoadPlayerInfoData();
		LoadPlayerSkillData();
		LoadItemData();
		LoadMonsterData();
		LoadMonsterSkillData();
		LoadExpDataTable();
		LoadStageData();
		LoadPlayerSkillTextData();
	}
	
	private IEnumerator StartLoadPlayerData<T>(string fileName,T table) where T : class
	{
		string path = string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, fileName);
		WWW www = new WWW(path);
		yield return www;
		string jsonString = www.text;
		JsonData jsonData = JsonMapper.ToObject(jsonString);
		table = JsonMapper.ToObject<T>(jsonData.ToJson());
		//참조왜안되지;
		if (table is PlayerData)
		{
			playerDataTable = table as PlayerData;
		}
		else playerInfoDataTable = table as PlayerInfoData;
		cDownCount++;
		if (cDownCount == AllDownCount)
		{
			UIMng.Ins.Setting();
		}
	}
	private IEnumerator StartLoad<T>(string fileName, Dictionary<int, T> table) where T : TableBase
	{
		string path = string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, fileName);
		WWW www = new WWW(path);
		yield return www;
		string jsonString = www.text;
		try
		{
			JsonData jsonData = JsonMapper.ToObject(jsonString);
			for (int i = 0; i < jsonData.Count; ++i)
			{
				T save = JsonMapper.ToObject<T>(jsonData[i].ToJson());
				table.Add(save.GetTableID(), save);
			}
			cDownCount++;
			if (cDownCount == AllDownCount)
			{
				UIMng.Ins.Setting();
			}
		}
		catch
		{
			Debug.Log(fileName);
			Debug.Log(jsonString);
		}
	}
	private IEnumerator StartLoad<T>(string fileName, Dictionary<int,List<T>> table) where T : TableBase
	{
		string path = string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, fileName);
		WWW www = new WWW(path);
		yield return www;
		string jsonString = www.text;
		JsonData jsonData = JsonMapper.ToObject(jsonString);
		for (int i = 0; i < jsonData.Count; ++i)
		{
			T save = JsonMapper.ToObject<T>(jsonData[i].ToJson());
			if(table.ContainsKey(save.GetTableID()))
			{
				table[save.GetTableID()].Add(save);
			}
			else
			{
				List<T> temp = new List<T>();
				temp.Add(save);
				table.Add(save.GetTableID(), temp);
			}
		}
		cDownCount++;
		if (cDownCount == AllDownCount)
		{
			UIMng.Ins.Setting();
		}
	}
	private void LoadItemData()
	{
		StartCoroutine(StartLoad("ItemDataTable", itemDataTable));
	}
	private void LoadPlayerData()
	{
		StartCoroutine(StartLoadPlayerData("PlayerDataTable", playerDataTable));
	}
	private void LoadPlayerInfoData()
	{
		StartCoroutine(StartLoadPlayerData("PlayerInfoDataTable", playerInfoDataTable));
	}
	private void LoadExpDataTable()
	{
		StartCoroutine(StartLoad("EXPDataTable", expDataTable));
	}
	private void LoadStageData()
	{
		StartCoroutine(StartLoad("StageDataTable", stageDataTable));
	}
	private void LoadMonsterData()
	{
		StartCoroutine(StartLoad("MonsterDataTable", monsterDataTable));
	}
	private void LoadPlayerSkillData()
	{
		StartCoroutine(StartLoad("PlayerSkillDataTable", playerSkillDataTable));
	}
	private void LoadMonsterSkillData()
	{
		StartCoroutine(StartLoad("MonsterSkillDataTable", monsterSkillDataTable));
	}
	private void LoadPlayerSkillTextData()
	{
		StartCoroutine(StartLoad("PlayerSkillTextDataTable", playerSkillTextDataTable));
	}
	public List<StageDataTable> GetStageData(int stagelevel)
	{
		return stageDataTable[stagelevel];
	}
	public int GetRandomSkillIndex()
	{
		int maxCount = playerSkillDataTable.Count;
		int cCount = 0;
		int endCount = Random.Range(1, maxCount);
		var e = playerSkillDataTable.GetEnumerator();
		while(cCount != endCount)
		{
			cCount++;
			e.MoveNext();
		}
		return e.Current.Value.skillID;
	}
}
