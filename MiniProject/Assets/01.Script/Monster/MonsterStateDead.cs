using GlobalDefine;
public class MonsterStateDead : MonsterState
{
	public MonsterStateDead(Monster o) : base(o)
	{

	}
	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Dead);
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
