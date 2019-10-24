using GlobalDefine;
public class MonsterStateIdle : MonsterState
{
	public MonsterStateIdle(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
	}

	public override bool OnTransition()
	{
		if (monsterObject.AttackCheckStart() == false)
		{
			monsterObject.monsterStateMachine.ChangeStateMove();
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;

		if (monsterObject.monsterStateMachine.IsAttack())
		{
			monsterObject.monsterStateMachine.ChangeStateAttack();
		}
	}
	public override void OnEnd()
	{

	}
}
