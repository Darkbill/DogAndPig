using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolymerMonsterStateMachine : StateMachine
{
    public PolymerMonster monster;
    private float skillDelayTime = 3.0f;
    private float skillCoolTime = 0.0f;
    private float skillPoisonSpikeDelayTime = 5.0f;
    private float skillPoisonSpikeCoolTime = 0.0f;
    public override void Setting()
    {
        monster.skill01 = Instantiate(Resources.Load(string.Format("Skill/MonsterSKill/Skill_Mucus"),
            typeof(SkillMucusAttack)) as SkillMucusAttack);
        monster.skill02 = Instantiate(Resources.Load(string.Format("Skill/MonsterSKill/Skill_PoisonSpike"),
            typeof(SkillPoisonSpike)) as SkillPoisonSpike);
        stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
        stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
        stateDict.Add(eMonsterState.SkillAttack, new PolymerSkillAttack(monster));
        stateDict.Add(eMonsterState.SkillAttack2, new PolymerSkillAttackPoisonSpike(monster));
        stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
        stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
        stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
        stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
        cState = stateDict[eMonsterState.Idle];
        cState.OnStart();
    }

    private void FixedUpdate()
    {
        skillCoolTime += Time.deltaTime;
        skillPoisonSpikeCoolTime += Time.deltaTime;
        cState.Tick();

        if (!SkillDelayCheck())
        {
            ChangeState(eMonsterState.SkillAttack);
            skillCoolTime = 0;
            skillDelayTime = Rand.Range(4, 7);
            return;
        }
        if(!SkillPoisionSpikeCheck())
        {
            ChangeState(eMonsterState.SkillAttack2);
            skillPoisonSpikeCoolTime = 0;
            skillPoisonSpikeDelayTime = Rand.Range(3, 9);
            return;
        }

        if (cState.GetType() == typeof(MonsterStateIdle))
        {
            UpdateState();
        }
    }

    public override void UpdateState()
    {
        if (monster.AttackDistanceCheck())
        {
            if (monster.AttackDelayCheck() && monster.AttackCheck())
            {
                ChangeStateAttack();
                return;
            }
        }
        else
        {
            ChangeStateMove();
        }
    }

    public void ChangeStateEndAttack()
    {
        ChangeStateIdle();
    }

    private bool SkillDelayCheck()
    {
        if (skillDelayTime >= skillCoolTime)
            return true;

        return false;
    }
    private bool SkillPoisionSpikeCheck()
    {
        if (skillPoisonSpikeDelayTime >= skillPoisonSpikeCoolTime)
            return true;
        return false;
    }
}
