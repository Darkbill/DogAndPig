using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMngInGame : MonoBehaviour
{
	public Image stickImage;
	private Vector3 stickPos;
	private float stickRadius = 80;
	private float heroSpeed = 0.25f; // TODO : HeroSpeed
	private bool isMove;
	private void Start()
	{
		stickPos = stickImage.gameObject.transform.position;
	}
	private void Update()
	{
		if (isMove)
		{
			MoveToJoyStick();
		}
	}
	private void MoveToJoyStick()
	{
		stickImage.gameObject.transform.position = Input.mousePosition;
		Vector3 dir = stickImage.gameObject.transform.position - stickPos;
		float m = dir.magnitude;
		dir.Normalize();
		GameMng.Ins.player.gameObject.transform.position += new Vector3(dir.x, dir.y, 0) * heroSpeed * 0.1f;
		if (m > stickRadius)
		{
			stickImage.gameObject.transform.position = stickPos + dir * stickRadius;
		}
	}
	public void OnStrickDrag()
	{
		isMove = true;
	}
	public void OnStickDrop()
	{
		isMove = false;
		stickImage.gameObject.transform.position = stickPos;
	}
}
