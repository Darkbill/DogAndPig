using UnityEngine;
using System.Collections.Generic;
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
public class Recorder : MonoBehaviour
{
	//필요한 이벤트 = 무브, 선택스킬, 발동방향, 몬스터의 움직임
	private List<EventValue> eventList = new List<EventValue>();
	private bool recordFlag = false;
	private float startTime;
	public RecordPlayer player;
	public SkillMng skillMng;
	
	private void Awake()
	{
		player.PlayerSetting();
		skillMng.LoadAll();
	}
	private void Update()
	{
		if (recordFlag == false && Input.GetKeyDown(KeyCode.Return))
		{
			recordFlag = true;
			startTime = Time.time;
		}
		if(recordFlag == false && Input.GetKeyDown(KeyCode.Escape))
		{
			player.MoveToEvent(eventList);
		}
		if(recordFlag)
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
			if (Input.GetMouseButtonDown(0))
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
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				recordFlag = false;
			}
		}
	}
}
