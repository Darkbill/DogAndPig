﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class PlayerStateMove : PlayerState
{
	public float fHorizontal;
	public float fVertical;
	public bool attackFlag = false;
	float delayTime;
	public Movement Mov = new Movement();

	public PlayerStateMove(Player o) : base(o)
	{
	}

	public override void OnStart()
	{

	}

	public override bool OnTransition()
	{
		//if (fVertical == 0 && fHorizontal == 0)
		//{
		//	return true;
		//}
		//return false;
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Idle);
			return;
		}
		Moving();
		Attack();
	}
	public override void OnEnd()
	{
        
	}
    private void Attack()
	{
		delayTime += Time.deltaTime;
		if (delayTime >= playerObject.calStat.attackSpeed)
		{
			ObjectSetAttack att = new ObjectSetAttack();
			var monsterPool = GameMng.Ins.monsterPool.monsterList;
			for (int i = 0; i < monsterPool.Count; ++i)
			{
				if (att.BaseAttack(playerObject.transform.right,
					monsterPool[i].gameObject.transform.position - playerObject.transform.position,
					playerObject.calStat.attackRange,
					playerObject.calStat.attackAngle))
				{
					delayTime = 0;
					if (Rand.Percent(playerObject.calStat.criticalChance))
					{
						monsterPool[i].Damage(eAttackType.Physics, playerObject.calStat.damage * playerObject.calStat.criticalDamage);
					}
					else
					{
						monsterPool[i].Damage(eAttackType.Physics, playerObject.calStat.damage);
					}
				}
			}
		}
	}
	

	private void Moving()
	{
		fHorizontal = Input.GetAxis("Horizontal");
		fVertical = Input.GetAxis("Vertical");

		Mov.iSpeed = playerObject.calStat.moveSpeed;
		playerObject.transform.position += Mov.Move(fHorizontal, fVertical);

		float Degree = Mathf.Atan2(fVertical, fHorizontal) * Mathf.Rad2Deg;
		playerObject.transform.eulerAngles = new Vector3(0, 0, Degree);
		
	}
}
