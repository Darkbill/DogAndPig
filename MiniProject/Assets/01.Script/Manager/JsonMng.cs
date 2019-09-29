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
	public Dictionary<int, StageData> StageDataTable { get; private set; } = new Dictionary<int, StageData>();
	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadAll();
	}
	private void LoadAll()
	{
		LoadMonsterData();
		LoadStageData();
	}
	public void LoadMonsterData()
	{
		LoadData("MonsterDataTable", monsterDataTable);
	}
	public void LoadStageData()
	{
		LoadData("StageDataTable", StageDataTable);
	}
	public void LoadData<T>(string fileName,Dictionary<int,T> table) where T : TableBase
	{
		string JsonString = File.ReadAllText(string.Format("{0}/Resources/LitJson/{1}.json", Application.dataPath, fileName));
		JsonData jsonData = JsonMapper.ToObject(JsonString);
		for (int i = 0; i < jsonData.Count; ++i)
		{
			T save = JsonMapper.ToObject<T>(jsonData[i].ToJson());
			table.Add(save.tableID, save);
		}
	}
	//public string FindNameToID<T>(int id, List<T> list) where T : DataBase
	//{
	//	var i = list.GetEnumerator();
	//	while (i.MoveNext())
	//	{
	//		if (i.Current.id == id) return i.Current.name;
	//	}
	//	return string.Empty;
	//}
}
