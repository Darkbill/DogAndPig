public class MilliMonsterStateDead : MonsterState
{
	public MilliMonsterStateDead(MilliMonster o) : base(o)
	{

	}
	public override void OnStart()
	{
		
	}

	public override bool OnTransition()
	{
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
