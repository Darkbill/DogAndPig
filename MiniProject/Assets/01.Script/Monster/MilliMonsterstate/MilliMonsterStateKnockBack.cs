using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilliMonsterStateKnockBack : MilliMonsterState
{
    private float setspeed = 10;

    private Vector3 range = new Vector3();
    

    public MilliMonsterStateKnockBack(MilliMonster o) : base(o)
    {
    }

    public override void OnStart()
    {
        setspeed = 10;
        range = monsterObject.transform.position -
            GameMng.Ins.player.transform.position;

        range.Normalize();
    }

    public override bool OnTransition()
    {
        return false;
    }

    public override void Tick()
    {
        if (setspeed > 0)
            KnockBack();
        else
            monsterObject.monsterStateMachine.ChangeState(eMilliMonsterState.Move);
        if (OnTransition() == true) return;
    }

    private void KnockBack()
    {
        monsterObject.transform.position += range * Time.deltaTime * setspeed;
        --setspeed;
        
    }

    public override void OnEnd()
    {

    }
}
