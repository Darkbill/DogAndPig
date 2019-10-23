using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateDead : MonsterState
{
    SkillBurningMeteor skill;
    public BossMonsterStateDead(BossMonster o) : base(o)
    {
        skill = o.BossSkill01;
    }

    public override void OnStart()
    {
        skill.SkillButtonOff();
    }

    public override bool OnTransition()
    {
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
