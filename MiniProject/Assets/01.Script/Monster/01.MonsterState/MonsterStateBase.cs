public abstract class MonsterStateBase
{
	public Monster monsterObject;
	public MonsterStateBase(Monster o)
	{
		monsterObject = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
