using GlobalDefine;
public class WizardMonsterStateAttack : MonsterState
{
    public WizardMonsterStateAttack(WizardMonster o) : base(o)
    {

    }
    public override void OnStart()
    {
		monsterObject.GetComponent<WizardMonster>().ShotMeteo();
		monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
		monsterObject.monsterStateMachine.ChangeStateIdle();
    }

    public override bool OnTransition()
    {
        return true;
    }

    public override void Tick()
    {
        //FindToAttackPlayer();
        if (OnTransition() == true) return;
    }
    public override void OnEnd()
    {

    }
    void FindToAttackPlayer()
    {
        //att.gameObject.transform.position = GameMng.Ins.player.transform.position;
        //att.StartingCount(1);
        //att.gameObject.SetActive(true);
    }

}
