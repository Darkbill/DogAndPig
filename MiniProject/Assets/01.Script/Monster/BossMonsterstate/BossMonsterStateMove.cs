using GlobalDefine;

public class BossMonsterStateMove : MonsterState
{
	public BossMonsterStateMove(BossMonster o) : base(o)
	{

	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Run);
	}

	public override bool OnTransition()
	{
		if (monsterObject.monsterStateMachine.IsAttack())
			monsterObject.monsterStateMachine.ChangeStateAttack();
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{

	}
}