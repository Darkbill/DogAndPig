using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// * protected * //
	protected int iHp;
	protected int iAtt;

	// * public * //
	public float fHorizontal { get; set; }
	public float fVertical { get; set; }
	public bool isMove;
	public Movement Mov;

	// * private * //
	private const int iBulletCnt = 20;
	protected int iSpeed;

	private void Awake()
	{
		PlayerSetting();
	}
	private void PlayerSetting()
	{
		iSpeed = 2;
	}
	private void Update()
	{
		if (isMove)
		{
			MoveToJoyStick();
		}
		else PlayerMove();
	}


	void PlayerMove()
	{
		fHorizontal = Input.GetAxis("Horizontal");
		fVertical = Input.GetAxis("Vertical");

		Mov.iSpeed = iSpeed;
		transform.position += Mov.Move(fHorizontal, fVertical);
	}
	public void MoveToJoyStick()
	{
		Vector3 direction = UIMngInGame.Ins.GetJoyStickDirection();
		gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * iSpeed * Time.deltaTime;
	}
	//TODO : 벽 부딪히면 넘어가지않게
	private void OnTriggerEnter2D(Collider2D collision)
	{

	}
}
