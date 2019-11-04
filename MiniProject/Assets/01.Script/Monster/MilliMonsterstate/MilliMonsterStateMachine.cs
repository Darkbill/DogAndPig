﻿using UnityEngine;
using GlobalDefine;
public class MilliMonsterStateMachine : MonsterStateMachine
{

	public override void Setting()
	{
		MilliMonster o = gameObject.GetComponent<MilliMonster>();

		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(o));
		stateDict.Add(eMonsterState.Move, new MilliMonsterStateMove(o));
		stateDict.Add(eMonsterState.SkillAttack, new MilliMonsterStateSkillAttack(o));
		stateDict.Add(eMonsterState.Attack, new MilliMonsterStateAttack(o));

		stateDict.Add(eMonsterState.Damage, new MonsterStateDamage(o));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(o));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(o));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(o));
        cState = stateDict[eMonsterState.Idle];
		cState.OnStart();
	}
	public override void ChangeStateKnockBack()
	{
		ChangeState(eMonsterState.KnockBack);
	}
	public override void ChangeStateKnockBack(Vector3 _knockBackDir,float knockBackPower)
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
