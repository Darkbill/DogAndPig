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

    public SkillCircleShot dummy;

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
		gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * calStat.moveSpeed * Time.deltaTime;

        float Degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, Degree);
	}

	public void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - calStat.armor);

		switch (attackType)
		{
			case eAttackType.Physics:
				d -= (int)(((1000 - calStat.physicsResist) * 0.001) + 0.5);
				break;
			case eAttackType.Fire:
				d -= (int)(((1000 - calStat.fireResist) * 0.001) + 0.5);
				break;
			case eAttackType.Water:
				d -= (int)(((1000 - calStat.waterResist) * 0.001) + 0.5);
				break;
			case eAttackType.Wind:
				d -= (int)(((1000 - calStat.windResist) * 0.001) + 0.5);
				break;
			case eAttackType.Lightning:
				d -= (int)(((1000 - calStat.lightningResist) * 0.001) + 0.5);
				break;
		}
		if (d < 1) d = 1;
		calStat.healthPoint -= (int)d;
		UIMngInGame.Ins.DamageToPlayer((int)d);
	}
	public void CalculatorStat()
	{
		//TODO : 레벨에 의한 스탯계산
		calStat = JsonMng.Ins.playerDataTable[1].AddStat(skillStat);
	}
	
	public float GetFullHP()
	{
		return skillStat.healthPoint + JsonMng.Ins.playerDataTable[level].healthPoint;
	}
	private void LevelUP()
	{
		CalculatorStat();
	}
}
