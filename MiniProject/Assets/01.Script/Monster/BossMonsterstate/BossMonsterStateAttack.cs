
using GlobalDefine;

public class BossMonsterStateAttack : MonsterState
{

    public BossMonsterStateAttack(BossMonster o) : base(o)
    {
       
    }
    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Attack);
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
