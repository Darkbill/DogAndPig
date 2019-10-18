using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateDead : BossMonsterState
{
    public BossMonsterStateDead(BossMonster o) : base(o)
    {

    }

    public override void OnStart()
    {
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
