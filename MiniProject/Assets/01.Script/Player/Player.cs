using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// * public * //
	public bool isMove;
	public int nowHealthPoint;
	public PlayerData playerData;
	public PlayerStateMachine playerStateMachine;

	// * private * //
	private int level = 1;

	public SpriteRenderer playerSprite;

    private void Awake()
	{
		PlayerSetting();
	}
	private void PlayerSetting()
	{
		playerData = JsonMng.Ins.playerDataTable[level];
		nowHealthPoint = (int)playerData.healthPoint;
	}
	private void Update()
	{
		if (isMove)
		{
			MoveToJoyStick();
		}
	}

    public void MoveToJoyStick()
	{
		Vector3 direction = UIMngInGame.Ins.GetJoyStickDirection();
		//TODO : speed X GetSpeed( stat + skillStat )
		gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * playerData.moveSpeed * Time.deltaTime;
	}

	public void Damage(int damage)
	{
		nowHealthPoint -= damage;
	}
}
