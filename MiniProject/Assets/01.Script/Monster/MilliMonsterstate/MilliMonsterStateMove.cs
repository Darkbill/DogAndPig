using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GlobalDefine;

public class MilliMonsterStateMove : MonsterState
{

	private float delaytime = 0.0f;
	Vector2 directionToPlayer;
	float degreeToPlayer;
	public MilliMonsterStateMove(MilliMonster o) : base(o)
	{

	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
		delaytime += Time.deltaTime;
		Debug.DrawRay(monsterObject.gameObject.transform.position, monsterObject.gameObject.transform.right);
		if (Mathf.Abs(degreeToPlayer) <= monsterObject.monsterData.attackAngle && directionToPlayer.magnitude <= monsterObject.monsterData.attackRange)
		{
			if (delaytime > monsterObject.monsterData.attackSpeed)
			{
				delaytime = 0.0f;
				monsterObject.Attack();
				monsterObject.monsterStateMachine.ChangeStateIdle();
				return true;
			}
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
		ChaseToPlayer();
	}
	public override void OnEnd()
	{

	}
	public void ChaseToPlayer()
	{
		Vector3 ownerDirection = monsterObject.gameObject.transform.right;
		directionToPlayer = GameMng.Ins.player.transform.position - monsterObject.gameObject.transform.position;
		float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
		float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
		degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

		if (degreeToPlayer > 180) degreeToPlayer -= 360;
		else if (degreeToPlayer < -180) degreeToPlayer += 360;

		if (degreeToPlayer < 0) 
            monsterObject.transform.eulerAngles += 
                new Vector3(0, 0, Time.deltaTime * monsterObject.monsterData.rotationSpeed);
		else 
            monsterObject.transform.eulerAngles -= 
                new Vector3(0, 0, Time.deltaTime * monsterObject.monsterData.rotationSpeed);
		if (directionToPlayer.magnitude > monsterObject.monsterData.attackRange)
		{
			monsterObject.gameObject.transform.position +=
				monsterObject.gameObject.transform.right *
				Time.deltaTime *
				monsterObject.monsterData.moveSpeed; 
                //monsterObject.condition.BufDebufUpdate().Speed;
		}
	}

}