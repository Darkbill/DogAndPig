using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeMonsterState
{
	public RangeMonsterStateMachine owner;
	public RangeMonsterState(RangeMonsterStateMachine o)
	{
		owner = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
