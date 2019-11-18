using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class RecordPlayer : Player
{
	enum eDownType
	{
		Left = 0,
		Right,
		Up,
		Down,
	}
	private bool isEventMove;
	private bool[] downState = new bool[4];
	private Vector3 startPos;
	private void Start()
	{
		startPos = gameObject.transform.position;
	}
	
	private void Update()
	{
		if (CheckMove())
		{
			Move();
		}
	}
	public void MoveToEvent(List<EventValue> eventList)
	{
		StartCoroutine(StartEventMove(eventList));
	}
	private IEnumerator StartEventMove(List<EventValue> eventList)
	{
		gameObject.transform.position = startPos;
		float cTime = 0;
		int indexCount = 0;
		while(true)
		{
			cTime += Time.deltaTime;
			if(eventList[indexCount].time <= cTime)
			{
				ChangePlayerMoveState(eventList[indexCount].inputStiring,eventList[indexCount].isdDown);
				indexCount++;
			}
			if (indexCount == eventList.Count) yield break;
			else yield return null;
		}
	}
	private void ChangePlayerMoveState(string inputString,bool isDown)
	{
		switch(inputString)
		{
			case "A":
				ChangeState(eDownType.Left, isDown);
				break;
			case "S":
				ChangeState(eDownType.Down, isDown);
				break;
			case "D":
				ChangeState(eDownType.Right, isDown);
				break;
			case "W":
				ChangeState(eDownType.Up, isDown);
				break;
			case "Skill":
				ActiveSkill();
				break;
		}
	}
	private void ActiveSkill()
	{
		Debug.Log("스킬");
		GameMng.Ins.skillMng.skillDict[1].ActiveSkill();
	}
	private void ChangeState(eDownType type, bool isDown)
	{
		downState[(int)type] = isDown;
	}
	private bool CheckMove()
	{
		for (int i = 0; i < downState.Length; ++i)
		{
			if (downState[i])
			{
				ChangeAnimation(GlobalDefine.ePlayerAnimation.Run);
				return true;
			}
		}
		ChangeAnimation(GlobalDefine.ePlayerAnimation.Idle);
		return false;
	}
	private void Move()
	{
		if (downState[(int)eDownType.Left])
		{
			gameObject.transform.position += Vector3.left * calStat.moveSpeed * Time.deltaTime;
		}
		else if (downState[(int)eDownType.Right])
		{
			gameObject.transform.position += Vector3.right * calStat.moveSpeed * Time.deltaTime;
		}
		else if (downState[(int)eDownType.Down])
		{
			gameObject.transform.position += Vector3.down * calStat.moveSpeed * Time.deltaTime;
		}
		else if (downState[(int)eDownType.Up])
		{
			gameObject.transform.position += Vector3.up * calStat.moveSpeed * Time.deltaTime;
		}
	}
}
