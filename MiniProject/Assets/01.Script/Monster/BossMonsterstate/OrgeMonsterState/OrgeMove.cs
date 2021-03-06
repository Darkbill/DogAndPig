﻿using UnityEngine;
using GlobalDefine;

public class OrgeMove : MonsterStateBase
{
	Vector2 directionToPlayer;
	float degreeToPlayer;
	Vector3 ownerDirection = new Vector3(1, 0, 0);

	public OrgeMove(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Run);
	}

	public override bool OnTransition()
	{

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
		ownerDirection = monsterObject.GetForward();
		directionToPlayer = GameMng.Ins.player.transform.position - monsterObject.gameObject.transform.position;
		float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
		float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
		degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

		if (degreeToPlayer > 180) { degreeToPlayer -= 360; }
		else if (degreeToPlayer < -180) { degreeToPlayer += 360; }

		if (Mathf.Abs(degreeToPlayer) > 1)
		{
			if (degreeToPlayer < 0)
				monsterObject.Angle +=
					Time.deltaTime * monsterObject.monsterData.rotationSpeed;
			else
				monsterObject.Angle -=
					Time.deltaTime * monsterObject.monsterData.rotationSpeed;
		}
		if (directionToPlayer.magnitude > monsterObject.monsterData.attackRange)
		{
			monsterObject.gameObject.transform.position +=
				ownerDirection *
				Time.deltaTime *
				monsterObject.monsterData.moveSpeed;
		}
		else
		{
			monsterObject.StateMachine.ChangeStateIdle();
		}
	}
}
