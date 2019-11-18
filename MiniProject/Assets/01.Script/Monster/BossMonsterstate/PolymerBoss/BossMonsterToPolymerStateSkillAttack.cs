using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterToPolymerStateSkillAttack : MonsterStateBase
{
    public BossMonsterToPolymerStateSkillAttack(BossMonsterToPolymer o) : base(o)
    {

    }
    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Skill);
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
