﻿using GlobalDefine;
using UnityEngine;
public class RangeMonsterStateMachine : StateMachine
{
	public RangeMonster monster;
	public override void Setting()
	{
		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
		stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
		stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
		cState = stateDict[eMonsterState.Idle];
		cState.OnStart();
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
	public override void ChangeStateKnockBack()
	{
		ChangeState(eMonsterState.KnockBack);
	}
    public override void ChangeStateKnockBack(Vector3 _knockBackDir, float knockBackPower)
    {
        base.ChangeStateKnockBack(_knockBackDir, knockBackPower);
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
		base.ChangeStateIdle();
	}
	public override void ChangeStateMove()
	{
		base.ChangeStateMove();
	}
	public override void ChangeStateDamage()
	{
		ChangeState(eMonsterState.Damage);
	}
}
