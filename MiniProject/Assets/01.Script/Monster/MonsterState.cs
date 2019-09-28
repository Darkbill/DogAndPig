using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState
{
	public MonsterStateMachine owner;
	public MonsterState(MonsterStateMachine o)
	{
		owner = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
