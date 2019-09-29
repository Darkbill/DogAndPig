using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateIdle : MonsterState
{
    private float delaytime;

	public MonsterStateIdle(MonsterStateMachine o) : base(o)
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
        if(delaytime > 1.0f)
        {
            owner.ChangeState(eMonsterState.Chase);
            return true;
        }

        return false;
		//if ()
		//{
		//	return true;
		//}
		//return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{

	}
}
