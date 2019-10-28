using UnityEngine;
using GlobalDefine;
public class GhoulMonsterStateMachine : MonsterStateMachine
{
	public override void Setting()
	{
		GhoulMonster o = gameObject.GetComponent<GhoulMonster>();

		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(o));
		stateDict.Add(eMonsterState.Move, new GhoulMonsterStateMove(o));
		stateDict.Add(eMonsterState.SkillAttack, new GhoulMonsterStateSkillAttack(o));
		stateDict.Add(eMonsterState.Attack, new GhoulMonsterStateAttack(o));

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
	public override void ChangeStateStun()
	{
		ChangeState(eMonsterState.Stun);
	}
	public override void ChangeStateAttack()
	{
		delayTime = 0;
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
