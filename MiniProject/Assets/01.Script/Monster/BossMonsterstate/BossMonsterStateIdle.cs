﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateIdle : MonsterState
{
    public BossMonsterStateIdle(BossMonster o) : base(o)
    {
    }

    public override void OnStart()
    {

    }

    public override bool OnTransition()
    {
        monsterObject.monsterStateMachine.ChangeStateMove();
        return false;
    }

    public override void Tick()
    {
        if (OnTransition() == true) return;
    }
    public override void OnEnd()
    {

    }
}
