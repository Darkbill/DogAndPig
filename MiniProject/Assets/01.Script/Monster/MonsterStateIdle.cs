using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateIdle : MonsterState
{
	public MonsterStateIdle(MonsterStateMachine o) : base(o)
	{
	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
		return true;
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
