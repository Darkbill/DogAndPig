﻿using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilliMonsterStateIdle : MilliMonsterState
{
	private float delaytime;

	public MilliMonsterStateIdle(MilliMonster o) : base(o)
	{
	}

	public override void OnStart()
	{
		delaytime = 0.0f;
	}

	public override bool OnTransition()
	{
		//TODO : 1초동안 멈춰있다가 Chase로 이동
		delaytime += Time.deltaTime;
		if (delaytime > 1.0f)
		{
			delaytime = 0.0f;
			monsterObject.monsterStateMachine.ChangeState(eMilliMonsterState.Move);
			return true;
		}

		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{

	}
}