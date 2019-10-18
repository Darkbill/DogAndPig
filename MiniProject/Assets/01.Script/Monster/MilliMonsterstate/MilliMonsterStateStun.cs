using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilliMonsterStateStun : MonsterState
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
		if (OnTransition() == true) return;
	}


    public override void OnEnd()
	{

	}
}
