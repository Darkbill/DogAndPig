public abstract class MilliMonsterState
{
	public MilliMonster monsterObject;
    public MilliMonsterState(MilliMonster o)
	{
		monsterObject = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
