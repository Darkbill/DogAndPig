using GlobalDefine;
using UnityEngine;
public class RangeMonsterStateMachine : MonsterStateMachine
{

	public override void Setting()
	{
		RangeMonster o = gameObject.GetComponent<RangeMonster>();

		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(o));
		stateDict.Add(eMonsterState.Move, new RangeMonsterStateMove(o));
		stateDict.Add(eMonsterState.SkillAttack, new RangeMonsterStateSkillAttack(o));
		stateDict.Add(eMonsterState.Attack, new RangeMonsterStateAttack(o));

		stateDict.Add(eMonsterState.Damage, new MonsterStateDamage(o));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(o));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(o));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(o));
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
