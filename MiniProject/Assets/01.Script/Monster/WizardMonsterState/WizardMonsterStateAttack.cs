using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMonsterStateAttack : MonsterState
{
    SkillWizardAttack att;
    public WizardMonsterStateAttack(WizardMonster o) : base(o)
    {
        o.AttackSet();
        att = o.attack;
        att.gameObject.SetActive(false);
    }
    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Attack);
        att.gameObject.SetActive(false);
    }

    public override bool OnTransition()
    {
        return true;
    }

    public override void Tick()
    {
        FindToAttackPlayer();
        if (OnTransition() == true) return;
    }
    public override void OnEnd()
    {

    }
    void FindToAttackPlayer()
    {
        //att.gameObject.transform.position = GameMng.Ins.player.transform.position;
        att.StartingCount(1);
        att.gameObject.SetActive(true);
    }

}
