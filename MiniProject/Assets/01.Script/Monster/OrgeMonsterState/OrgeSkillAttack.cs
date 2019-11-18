﻿using GlobalDefine;
using UnityEngine;
public class OrgeSkillAttack : MonsterStateBase
{
	private const float holdTime = 1f;
	private const float moveSpeed = 5f;
	private float cTime;
	private Vector3 toPlayerDir;
	private Vector3 endPos;
	private Vector3 moveDir;
	public OrgeSkillAttack(OrgeMonster o) : base(o)
	{

	}
	public override void OnStart()
	{
		cTime = 0;
		endPos = GameMng.Ins.player.transform.position;
		monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
		toPlayerDir = GameMng.Ins.player.transform.position -
				monsterObject.transform.position +
				new Vector3(0, GameMng.Ins.player.calStat.size, 0);
		toPlayerDir.z = 0;
		moveDir = toPlayerDir.normalized;
		Debug.Log(moveDir);
	}
	public override bool OnTransition()
	{
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
		cTime += Time.deltaTime;
		if(cTime>= holdTime)
		{
			if(CheckDistance())
			{
				monsterObject.ChangeAnimation(eMonsterAnimation.Skill);
				monsterObject.StateMachine.ChangeStateIdle();
			}
			else
			{
				monsterObject.transform.position += moveDir * moveSpeed * Time.deltaTime;
			}
		}
	}
	private bool CheckDistance()
	{
		float m = (monsterObject.transform.position - endPos).magnitude;
		if(m <= 0.25f)
		{
			return true;
		}
		return false;
	}
	public override void OnEnd()
	{

	}
}
