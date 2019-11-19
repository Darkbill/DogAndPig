using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System.IO;
public struct EventValue
{
	public string inputStiring;
	public float time;
	public bool isdDown;

	public EventValue(string s,float f,bool b)
	{
		inputStiring = s;
		time = f;
		isdDown = b;
	}
}
public struct MonsterEventValue
{
	public string inputStiring;
	public float time;
	public bool isdDown;
	public MonsterEventValue(string s, float f, bool b)
	{
		inputStiring = s;
		time = f;
		isdDown = b;
	}
}
public struct MouseEventValue
{
	public float xPos;
	public float yPos;
	public float time;
	public bool isdDown;
	public MouseEventValue(Vector3 v, float f, bool b)
	{
		xPos = v.x;
		yPos = v.y;
		time = f;
		isdDown = b;
	}
}
public class Recorder : MonoBehaviour
{
	//필요한 이벤트 = 무브, 선택스킬, 발동방향, 몬스터의 움직임
	private List<EventValue> eventList = new List<EventValue>();
	private List<MouseEventValue> mouseEventList = new List<MouseEventValue>();
	private List<MonsterEventValue> monsterEventList = new List<MonsterEventValue>();
	private bool recordFlag = false;
	private float startTime;
	public RecordPlayer player;
	public SkillMng skillMng;
	private int skillID = 18;
	private void Awake()
	{

	}
	private void Update()
	{
		if (recordFlag == false && Input.GetKeyDown(KeyCode.Return))
		{
			recordFlag = true;
			startTime = Time.time;
		}
		if(recordFlag == false && Input.GetKeyDown(KeyCode.F1))
		{
			player.MoveToEvent(eventList,mouseEventList, skillID);
		}
		if(recordFlag == false && Input.GetKeyDown(KeyCode.F2))
		{
			SaveAll();
		}
		if (recordFlag == false && Input.GetKeyDown(KeyCode.F3))
		{
			player.MoveToEvent(skillID);
		}
		if (recordFlag)
		{ 
			if (Input.GetKeyDown(KeyCode.A))
			{
				eventList.Add(new EventValue("A", Time.time - startTime, true));
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				eventList.Add(new EventValue("S", Time.time - startTime, true));
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				eventList.Add(new EventValue("D", Time.time - startTime, true));
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				eventList.Add(new EventValue("W", Time.time - startTime, true));
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				eventList.Add(new EventValue("Skill", Time.time - startTime, true));
			}
			if (Input.GetKeyUp(KeyCode.A))
			{
				eventList.Add(new EventValue("A", Time.time - startTime, false));
			}
			if (Input.GetKeyUp(KeyCode.S))
			{
				eventList.Add(new EventValue("S", Time.time - startTime, false));
			}
			if (Input.GetKeyUp(KeyCode.D))
			{
				eventList.Add(new EventValue("D", Time.time - startTime, false));
			}
			if (Input.GetKeyUp(KeyCode.W))
			{
				eventList.Add(new EventValue("W", Time.time - startTime, false));
			}
			if (Input.GetKeyUp(KeyCode.F1))
			{
				recordFlag = false;
			}
			if(Input.GetMouseButtonDown(0))
			{
				mouseEventList.Add(new MouseEventValue(Input.mousePosition, Time.time - startTime, true));
			}
			if (Input.GetMouseButtonUp(0))
			{
				mouseEventList.Add(new MouseEventValue(Input.mousePosition, Time.time - startTime, false));
			}
		}
	}
	private void SaveAll()
	{
		RecordData d = new RecordData();
		d.skillID = skillID;
		d.playerEventList = eventList;
		d.mouseEventList = mouseEventList;
		d.monsterEvenetList = monsterEventList;
		var i = JsonMng.Ins.recordDataTable.GetEnumerator();
		List<RecordData> temp = new List<RecordData>();
		while(i.MoveNext())
		{
			temp.Add(i.Current.Value);
		}
		temp.Add(d);
		JsonData data = JsonMapper.ToJson(temp);
		File.WriteAllText(string.Format("{0}/LitJson/{1}.json", Application.streamingAssetsPath, "RecordDataTable"), data.ToString());
	}
}
