using GlobalDefine;

public class MilliMonsterStateIdle : MonsterState
{

	public MilliMonsterStateIdle(MilliMonster o) : base(o)
	{
	}

	public override void OnStart()
	{
        monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
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
