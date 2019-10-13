using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public eAttackType AttackType = eAttackType.Physics;
    public int SkillID = 0;
    public float MaxTimer = 0.0f;
    public float SetTimer = 0.0f;
    public float OldTimer = 0.0f;

    public float Speed = 1.0f;
    public float Damage = 0.0f;
    public bool NonAction = false;

    public State()
    {

    }
    public State(eAttackType type, int id, float maxtimer, Vector3 statebase)
    {
        AttackType = type;
        SkillID = id;
        MaxTimer = maxtimer;

        Speed = statebase.x;
        Damage = statebase.y;
        NonAction = statebase.z <= 0 ? false : true;
    }

    public bool CompareTimer() { return MaxTimer > SetTimer; }
}