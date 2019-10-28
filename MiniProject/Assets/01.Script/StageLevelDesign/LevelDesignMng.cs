using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using UnityEngine.UI;
public class LevelDesignMng : MonoBehaviour
{
	#region SINGLETON
	static LevelDesignMng _instance = null;

	public static LevelDesignMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(LevelDesignMng)) as LevelDesignMng;
				if (_instance == null)
				{
					_instance = new GameObject("LevelDesignMng", typeof(LevelDesignMng)).GetComponent<LevelDesignMng>();
				}
			}

			return _instance;
		}
	}
	#endregion
	public Dropdown dropDown;
	public MonsterExam monsterExam;
	public GameObject gameView;
	public List<MonsterExam> monsterExamList;
	public Dictionary<int, List<StageDataTable>> stageDataTable { get; private set; } = new Dictionary<int, List<StageDataTable>>();

	public Text monsterIndex;
	public Text monsterLevel;
	public Toggle monsterToggle;
	public Text stageText;
	private int lastID;
	private void Awake()
	{
		LoadStageData();
		//string path = string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, "StageDataTable");
		//StageDataTable s = new StageDataTable(10,10,10,10,5.5f,5.5f,5);
		//string jsonData = JsonMapper.ToJson(s);
		//File.WriteAllText(path, jsonData);
	}
	private void Setting()
	{
		dropDown.ClearOptions();
		var i = stageDataTable.GetEnumerator();
		List<int> stage = new List<int>();
		while(i.MoveNext())
		{
			dropDown.options.Add(new Dropdown.OptionData() { text = i.Current.Key.ToString() });
		}
		ShowStageInfo(1);
		dropDown.value = 0;
	}
	public void ShowStageInfo(int stageNumber)
	{
		RemoveMonsterExamList();
		List<StageDataTable> stageList = stageDataTable[stageNumber];
		for(int i = 0; i < stageList.Count; ++i)
		{
			float xPos = stageList[i].enemyPosX;
			float yPos = stageList[i].enemyPosY;
			int monsterIndex = stageList[i].enemyIndex;
			int monsterLevel = stageList[i].enemyLevel;
			GameObject o = Instantiate(monsterExam.gameObject, gameView.transform);
			MonsterExam e = o.GetComponent<MonsterExam>();
			e.Setting(monsterLevel.ToString(), monsterIndex.ToString(),xPos,yPos,stageList[i].boss);
			monsterExamList.Add(e);
		}
	}
	public void SaveStage()
	{
		List<StageDataTable> tempList = new List<StageDataTable>();
		for (int i = 0; i < monsterExamList.Count; ++i)
		{
			if (monsterExamList[i] == null) continue;
			StageDataTable temp = new StageDataTable();
			temp.stageID = ++lastID;
			temp.stageLevel = dropDown.value + 1;
			temp.enemyIndex = int.Parse(monsterExamList[i].indexText.text);
			temp.enemyLevel = int.Parse(monsterExamList[i].levelText.text);
			Vector2 pos = monsterExamList[i].GetCoord();
			temp.enemyPosX = pos.x;
			temp.enemyPosY = pos.y;
			tempList.Add(temp);
		}
		if(stageDataTable.ContainsKey(dropDown.value+1))
		{
			stageDataTable[dropDown.value+1] = tempList;
		}
		else
		{
			stageDataTable.Add(dropDown.value+1, tempList);
		}
	}
	private void RemoveMonsterExamList()
	{
		for(int i = 0; i < monsterExamList.Count; ++i)
		{
			Destroy(monsterExamList[i].gameObject);
		}
		monsterExamList.Clear();
	}
	private void LoadStageData()
	{
		StartCoroutine(StartLoad("StageDataTable", stageDataTable));
	}
	private IEnumerator StartLoad(string fileName, Dictionary<int, List<StageDataTable>> table)
	{
		string path = string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, fileName);
		WWW www = new WWW(path);
		yield return www;
		string jsonString = www.text;
		JsonData jsonData = JsonMapper.ToObject(jsonString);
		for (int i = 0; i < jsonData.Count; ++i)
		{
			var save = JsonMapper.ToObject<StageDataTable>(jsonData[i].ToJson());
			if(table.ContainsKey(save.stageLevel))
			{
				table[save.stageLevel].Add(save);
			}
			else
			{
				List<StageDataTable> temp = new List<StageDataTable>();
				temp.Add(save);
				table.Add(save.stageLevel, temp);
				lastID = save.stageID;
			}
		}
		Setting();
	}
	public void SaveAll()
	{
		string path = string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, "StageDataTable");
		var i = stageDataTable.GetEnumerator();
		string jsonData = "";
		while(i.MoveNext())
		{
			for(int j = 0; j < i.Current.Value.Count; ++j)
			{
				JsonData data = JsonMapper.ToJson(i.Current.Value[j]);
				jsonData = string.Format("{0}{1}", jsonData, data.ToString());
			}
		}
		File.WriteAllText(path, jsonData);
	}
	public void RemoveStage()
	{
		stageDataTable.Remove(dropDown.value+1);
		Setting();
	}
	public void CreateStage()
	{
		if(stageDataTable.ContainsKey(int.Parse(stageText.text)))
		{
			Debug.LogError("이미 존재하는 Stage");
		}
		else
		{
			stageDataTable.Add(int.Parse(stageText.text), new List<StageDataTable>());
			Setting();
		}
		stageText.text = "";
	}
	public void CreateMonster()
	{
		float xPos = 0;
		float yPos = 0;
		GameObject o = Instantiate(monsterExam.gameObject, gameView.transform);
		MonsterExam e = o.GetComponent<MonsterExam>();
		int boss;
		if (monsterToggle.isOn) boss = 1;
		else boss = 0;
		e.Setting(monsterLevel.text, monsterIndex.text, xPos, yPos, boss);
		monsterExamList.Add(e);
		monsterLevel.text = "";
		monsterIndex.text = "";
	}
}
