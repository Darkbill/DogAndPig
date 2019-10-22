using GlobalDefine;
public class MonsterStateDamage : MonsterState
{
	public MonsterStateDamage(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Hit);
	}

	public override bool OnTransition()
	{
		monsterObject.monsterStateMachine.ChangeStateMove();
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
