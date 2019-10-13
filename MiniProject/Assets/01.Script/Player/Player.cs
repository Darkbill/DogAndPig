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
	/* 테스트 코드 */
	public int[] skillArr = new int[2] { 0, 1 };
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
		float resist;
		switch (attackType)
		{
			case eAttackType.Physics:
				resist = calStat.physicsResist;
				GetDamage(resist);
				break;
			case eAttackType.Fire:
				resist = calStat.fireResist;
				GetDamage(resist);
				break;
			case eAttackType.Water:
				resist = calStat.waterResist;
				GetDamage(resist);
				break;
			case eAttackType.Wind:
				resist = calStat.windResist;
				GetDamage(resist);
				break;
			case eAttackType.Lightning:
				resist = calStat.lightningResist;
				GetDamage(resist);
				break;
		}
		if (d < 1) d = 1;
		calStat.healthPoint -= (int)d;
		UIMngInGame.Ins.DamageToPlayer((int)d);
		if (calStat.healthPoint <= 0) GameMng.Ins.GameOver();
	}
	public int GetDamage(float resist)
	{
		if (resist >= 500)
		{
			return (int)(((1500 - calStat.physicsResist) * 0.001) * 1.6f - 0.6f);
		}
		else
		{
			return (int)(((500 - calStat.physicsResist) * 0.001) * 8 - 1);
		}
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
