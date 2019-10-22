public class BossMonsterStateMove : MonsterState
{
    public BossMonsterStateMove(BossMonster o) : base(o)
    {

    }

    public override void OnStart()
    {
    }

    public override bool OnTransition()
    {
        monsterObject.monsterStateMachine.ChangeStateAttack();
        return false;
    }

    public override void Tick()
    {
        if (OnTransition() == true) return;
        ChaseToPlayer();
    }
    public override void OnEnd()
    {

    }
    public void ChaseToPlayer()
    {

    }
}
