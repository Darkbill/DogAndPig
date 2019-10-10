using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// * public * //
	public bool isMove;
	public PlayerData calStat;
	public PlayerData skillStat = new PlayerData();
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
		CalculatorStat();
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
		gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * calStat.moveSpeed * Time.deltaTime;
	}

	public void Damage(int damage)
	{
		calStat.healthPoint -= damage;
	}

	public void CalculatorStat()
	{
		calStat = JsonMng.Ins.playerDataTable[level].AddStat(skillStat);
	}
	
	public float GetFullHP()
	{
		return skillStat.healthPoint + JsonMng.Ins.playerDataTable[level].healthPoint;
	}
}
