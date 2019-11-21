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
	public int skillID;
	private void Start()
	{
		startPos = gameObject.transform.position;
	}
	private void Update()
	{
		//움직임과 스킬 사용만을 재동작
		if (CheckMove())
		{
			Move();
		}
	}
	public void MoveToEvent(int _skillID)
	{
		//전체상태 초기화
		GameMng.Ins.skillMng.OffSkill();
		GameMng.Ins.monsterPool.Reset();
		gameObject.transform.position = startPos;
		skillID = _skillID;
		//재생 스킬에 맞는 키 입력 시간, 정보 로드
		var i = JsonMng.Ins.recordDataTable[skillID];
		var e = i.playerEventList;
		var m = i.mouseEventList;
		StartCoroutine(StartEventMove(e, m));
	}
	public void MoveToEvent(List<EventValue> eventList,List<MouseEventValue> mouseEventList,int _skillID)
	{
		StartCoroutine(StartEventMove(eventList,mouseEventList));
		skillID = _skillID;
	}
	private IEnumerator StartEventMove(List<EventValue> eventList,List<MouseEventValue> mouseEventList)
	{
		float cTime = 0;
		int indexCount = 0;
		int mouseIndexCount = 0;
		while(true)
		{
			//시간을 비교하여 마우스, 키입력 이벤트에 맞는 함수 실행
			cTime += Time.deltaTime;
			if (indexCount < eventList.Count && eventList[indexCount].time <= cTime)
			{
				ChangePlayerMoveState(eventList[indexCount].inputStiring,eventList[indexCount].isdDown);
				indexCount++;
			}
			if (mouseIndexCount < mouseEventList.Count && mouseEventList[mouseIndexCount].time <= cTime)
			{
				ChangeMouserState(new Vector2(mouseEventList[mouseIndexCount].xPos, mouseEventList[mouseIndexCount].yPos), mouseEventList[mouseIndexCount].isdDown);
				mouseIndexCount++;
			}
			if (indexCount == eventList.Count && mouseIndexCount == mouseEventList.Count) yield break;
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
				GameMng.Ins.skillMng.skillDict[skillID].OnButtonDown();
				break;
		}
	}
	private void ChangeMouserState(Vector3 pos, bool isDown)
	{
		if(isDown) GameMng.Ins.skillMng.skillDict[skillID].OnDrag();
		else GameMng.Ins.skillMng.skillDict[skillID].OnDrop(pos);
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
