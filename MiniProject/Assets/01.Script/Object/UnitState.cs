using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState
{
    const int BufDebufMaxCount = 20;

    public State[] SkillStatePoket = new State[BufDebufMaxCount];

    public bool SkillBufDebuf(State state)
    {
        for(int i = 0; i < BufDebufMaxCount; ++i)
        {
            if (SkillStatePoket[i] != null)
            {
                if (SkillStatePoket[i].SkillID == state.SkillID)
                {
                    SkillStatePoket[i].SetTimer = 0.0f;
                    return true;
                }
                if (SkillStatePoket[i].SkillID == 0 && SkillStatePoket[i] == null)
                {
                    SkillStatePoket[i] = state;
                    return true;
                }
            }
            else
            {
                SkillStatePoket[i] = state;
                return true;
            }
        }
        return false;
    }

    public State BufDebufUpdate()
    {
        State set = new State();

        for (int i = 0; i < BufDebufMaxCount; ++i)
        {
            if (SkillStatePoket[i] != null)
            {
                if (SkillStatePoket[i].SkillID != 0)
                {
                    SkillStatePoket[i].SetTimer += Time.deltaTime;
                    set = Set(SkillStatePoket[i], set);
                    if (!SkillStatePoket[i].CompareTimer())
                    {
                        SkillStatePoket[i].SkillID = 0;
                    }
                }
            }
        }
        return set;
    }

    private State Set(State state, State set)
    {
        switch(state.AttackType)
        {
            case eAttackType.Physics:
                set.NonAction = state.NonAction; return set;
            case eAttackType.Water:
                set.Speed = set.Speed * (1 - state.Speed); return set; 
            case eAttackType.Fire:
                set.Damage += state.Damage; return set; 
        }
        return set;
    }
}
