using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilliMonsterStateStun : MilliMonsterState
{
    private float SetTime = 0.0f;
	public MilliMonsterStateStun(MilliMonster o) : base(o)
	{

	}
	public override void OnStart()
	{
        SetTime = 0.0f;
    }

	public override bool OnTransition()
	{
        SetTime += Time.deltaTime;
		return true;
	}

	public override void Tick()
	{
        StunTime();
		if (OnTransition() == true) return;
	}

    private void StunTime()
    {
        if (SetTime >= 2.0f)
            monsterObject.monsterStateMachine.ChangeState(GlobalDefine.eMilliMonsterState.Move);
    }

    public override void OnEnd()
	{

	}
}
