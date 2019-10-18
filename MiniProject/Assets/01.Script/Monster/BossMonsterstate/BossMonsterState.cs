using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMonsterState
{
    public BossMonster monsterObject;
    public BossMonsterState(BossMonster o)
    {
        monsterObject = o;
    }
    public abstract void OnStart();
    public abstract void Tick();
    public abstract void OnEnd();
    public abstract bool OnTransition();
}
