using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	//카메라 초기위치와 진동 도달위치
	private Vector3 startPos;
	private Vector3 goalPos = Vector3.zero;
	private float currentTime;
	private float endTime;
	public float magnitube;
	private float speed;
	private bool isreturn;
	private Vector3 dir;

	public void OnStart()
	{
		//테스트코드
		startPos = new Vector3(0, 0, -10);
		//현위치대비 이동할 랜덤위치 -> 도달시 첫위치
		goalPos = Vector3.zero;
		//랜덤위치 도달시 돌아가게 할 플래그
		isreturn = false;
		currentTime = 0;
		endTime = 0.25f;
		//흔들리는 크기
		magnitube = 0.25f;
		speed = 1;
		StartCoroutine(StartCameraMove());
	}
	IEnumerator StartCameraMove()
	{
		while (true)
		{
			currentTime += Time.deltaTime;
			if (isreturn)
			{
				Return();
			}
			else
			{
				GoToGoal();
			}
			if (OnEnd() == true) break;
			else yield return null;
		}
	}
	public bool OnEnd()
	{
		if (currentTime >= endTime)
		{
			gameObject.transform.position = startPos;
			return true;
		}
		return false;
	}
	private void GoToGoal()
	{
		if (goalPos == Vector3.zero) SetPosition();
		gameObject.transform.position += new Vector3(dir.x * speed, dir.y * speed, 0);
		if ((goalPos - gameObject.transform.position).magnitude < 0.5f)
		{
			isreturn = true;
			goalPos = Vector3.zero;
		}
	}
	private void SetPosition()
	{
		float fX = Random.Range(-0.25f, 0.25f) * magnitube;
		float fY = Random.Range(-0.25f, 0.25f) * magnitube;
		goalPos = new Vector3(startPos.x + fX, startPos.y + fY, startPos.z);
		dir = (goalPos - gameObject.transform.position).normalized * 0.1f;
	}
	private void Return()
	{
		if (goalPos == Vector3.zero) SetStartPosition();
		gameObject.transform.position += new Vector3(dir.x * speed, dir.y * speed, 0);
		if ((goalPos - gameObject.transform.position).magnitude < 0.5f)
		{
			isreturn = false;
			goalPos = Vector3.zero;
		}
	}
	private void SetStartPosition()
	{
		goalPos = startPos;
		dir = (goalPos - gameObject.transform.position).normalized * 0.2f;
	}
	public void GameOver()
	{
		currentTime = endTime;
	}
}
