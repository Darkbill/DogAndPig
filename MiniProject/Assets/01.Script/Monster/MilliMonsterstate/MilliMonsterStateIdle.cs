using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilliMonsterStateIdle : MonsterState
{
	private float delaytime;

	public MilliMonsterStateIdle(MilliMonster o) : base(o)
	{
	}

	public override void OnStart()
	{
		delaytime = 0.0f;
        monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
    }

	public override bool OnTransition()
	{
		delaytime += Time.deltaTime;
		if (delaytime >= 1.0f)
		{
			monsterObject.monsterStateMachine.ChangeStateMove();
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
