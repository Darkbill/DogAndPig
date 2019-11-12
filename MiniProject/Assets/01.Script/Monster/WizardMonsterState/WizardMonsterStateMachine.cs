﻿using GlobalDefine;

public class WizardMonsterStateMachine : StateMachine
{
	public WizardMonster monster;
	public override void Setting()
    {

        stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
        stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
        stateDict.Add(eMonsterState.SkillAttack, new WizardMonsterStateSkillAttack(monster));
        stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
        stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
        stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
        stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
        cState = stateDict[eMonsterState.Idle];
        cState.OnStart();
    }
	public override void UpdateState()
	{
		if (monster.AttackDistanceCheck())
		{
			if (monster.AttackDelayCheck())
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
	public override void ChangeStateKnockBack()
    {
        ChangeState(eMonsterState.KnockBack);
    }
    public override void ChangeStateStun()
    {
        ChangeState(eMonsterState.Stun);
    }
    public override void ChangeStateAttack()
    {
        ChangeState(eMonsterState.Attack);
    }
    public override void ChangeStateDead()
    {
        ChangeState(eMonsterState.Dead);
    }
    public override void ChangeStateIdle()
    {
        ChangeState(eMonsterState.Idle);
    }
    public override void ChangeStateMove()
    {
        ChangeState(eMonsterState.Move);
    }
    public override void ChangeStateDamage()
    {
        ChangeState(eMonsterState.Damage);
    }
}
