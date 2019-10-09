using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GlobalDefine;

public class MonsterStateChase : MonsterState
{

    private float delaytime = 0.0f;
	Vector2 directionToPlayer;
	float degreeToPlayer;
	public MonsterStateChase(MonsterStateMachine o) : base(o)
	{

	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
        delaytime += Time.deltaTime;
		Debug.DrawRay(owner.gameObject.transform.position, owner.gameObject.transform.right * 2);
		if (Mathf.Abs(degreeToPlayer) <= owner.monsterData.attackAngle && directionToPlayer.magnitude <= owner.monsterData.attackRange)
		{
			if (delaytime > owner.monsterData.attackSpeed)
			{
				delaytime = 0.0f;
				owner.Attack();
				owner.ChangeState(eMonsterState.Idle);
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
		Vector3 ownerDirection = owner.gameObject.transform.right;
		directionToPlayer = GameMng.Ins.player.transform.position - owner.gameObject.transform.position;
		float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
		float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
		degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

		if (degreeToPlayer > 180)		degreeToPlayer -= 360;
		else if (degreeToPlayer < -180) degreeToPlayer += 360;

		if (degreeToPlayer < 0) owner.transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * owner.monsterData.rotationSpeed);
		else					owner.transform.eulerAngles -= new Vector3(0, 0, Time.deltaTime * owner.monsterData.rotationSpeed);

		if (directionToPlayer.magnitude > owner.monsterData.attackRange)
		{
			owner.gameObject.transform.position += owner.gameObject.transform.right * Time.deltaTime * owner.monsterData.moveSpeed;
		}
	}

}
