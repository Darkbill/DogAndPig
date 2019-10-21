using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateSkillAttack : MonsterState
{
    SkillBurningMeteor skill;

    public BossMonsterStateSkillAttack(BossMonster o) : base(o)
    {
        skill = o.BossSkill01;
    }
    public override void OnStart()
    {
        skill.SkillSetting();
        skill.SkillButtonOn();
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
