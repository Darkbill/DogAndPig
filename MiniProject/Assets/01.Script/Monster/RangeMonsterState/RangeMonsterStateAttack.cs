using GlobalDefine;
public class RangeMonsterStateAttack : MonsterState
{
	public RangeMonsterStateAttack(RangeMonster o) : base(o)
	{

	}
	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Attack);
        monsterObject.AttackCheckStart();
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
		monsterObject.monsterStateMachine.delayTime = 0;
	}
}
