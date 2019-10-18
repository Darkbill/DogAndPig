using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState
{
	public Monster monsterObject;
	public MonsterState(Monster o)
	{
		monsterObject = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
