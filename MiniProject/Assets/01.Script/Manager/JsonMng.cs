using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	public Dictionary<int, PlayerData> playerDataTable { get; private set; } = new Dictionary<int, PlayerData>();
	public Dictionary<int, PlayerSkillData> playerSkillDataTable { get; private set; } = new Dictionary<int, PlayerSkillData>();
	public Dictionary<int, MonsterData> monsterDataTable { get; private set; } = new Dictionary<int, MonsterData>();
	public Dictionary<int, MonsterSkillData> monsterSkillDataTable { get; private set; } = new Dictionary<int, MonsterSkillData>();
	public Dictionary<int, StageDataTable> stageDataTable { get; private set; } = new Dictionary<int, StageDataTable>();
	public Dictionary<int, EXPData> expDataTable { get; private set; } = new Dictionary<int, EXPData>();
	public PlayerInfoData playerInfoDataTable { get; private set; } = new PlayerInfoData();
	private int AllDownCount = 7;
	private int cDownCount = 0;
	public bool IsDone
	{
		get { if (AllDownCount == cDownCount) return true;
			else return false;
		}
	}
	//테스트 코드
	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadAll();
	}
	private void LoadAll()
	{
		StartCoroutine(StartLoadPlayerData());
		LoadPlayerData();
		LoadPlayerSkillData();
		LoadMonsterData();
		LoadMonsterSkillData();
		LoadExpDataTable();
		LoadStageData();
	}
	private IEnumerator StartLoadPlayerData()
	{
		string path = string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, "PlayerInfoDataTable");
		WWW www = new WWW(path);
		yield return www;
		string jsonString = www.text;
		JsonData jsonData = JsonMapper.ToObject(jsonString);
		playerInfoDataTable = JsonMapper.ToObject<PlayerInfoData>(jsonData.ToJson());
		cDownCount++;
	}
	private IEnumerator StartLoad<T>(string fileName, Dictionary<int, T> table) where T : TableBase
	{
		string path = string.Format("{0}/LitJson/{1}.json",Application.streamingAssetsPath, fileName);
		WWW www = new WWW(path);
		yield return www;
		string jsonString = www.text;
		JsonData jsonData = JsonMapper.ToObject(jsonString);
		for (int i = 0; i < jsonData.Count; ++i)
		{
			T save = JsonMapper.ToObject<T>(jsonData[i].ToJson());
			table.Add((int)save.GetTableID(), save);
		}
		cDownCount++;
		if(cDownCount == AllDownCount)
		{
			UIMng.Ins.Setting();
		}
	}
	private void LoadExpDataTable()
	{
		StartCoroutine(StartLoad("EXPDataTable", expDataTable));
	}
	private void LoadStageData()
	{
		StartCoroutine(StartLoad("StageDataTable", stageDataTable));
	}
	private void LoadPlayerData()
	{
		StartCoroutine(StartLoad("PlayerDataTable", playerDataTable));
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

    public List<StageDataTable> GetStageData(int stagelevel)
    {
        List<StageDataTable> lv = new List<StageDataTable>();
        for(int i = 1;i<stageDataTable.Count+1;++i)
        {
            if (stageDataTable[i].stageLevel == stagelevel)
                lv.Add(stageDataTable[i]);
        }
        return lv;
    }
}
