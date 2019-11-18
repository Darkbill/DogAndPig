using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterToPolymerStateMachine : StateMachine
{
    public BossMonsterToPolymer monster;
    private float skillDelayTime = 5.0f;
    private float skillCoolTime = 0.0f;
    public override void Setting()
    {
        monster.skill01 = Instantiate(Resources.Load(string.Format("Skill/MonsterSKill/Skill_Mucus"),
            typeof(SkillMucusAttack)) as SkillMucusAttack);
        stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
        stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
        stateDict.Add(eMonsterState.SkillAttack, new BossMonsterToPolymerStateSkillAttack(monster));
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
        cState.Tick();

        if(!SkillDelayCheck())
        {
            ChangeState(eMonsterState.SkillAttack);
            skillCoolTime = 0;
            skillDelayTime = Rand.Range(5, 11);
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
}
