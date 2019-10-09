using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateAttack : MonsterState
{

	public MonsterStateAttack(MonsterStateMachine o) : base(o)
	{

	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
		
        owner.ChangeState(eMonsterState.Chase);

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
