﻿public abstract class RangeMonsterState
{
	public RangeMonster monsterObject;
	public RangeMonsterState(RangeMonster o)
	{
		monsterObject = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}