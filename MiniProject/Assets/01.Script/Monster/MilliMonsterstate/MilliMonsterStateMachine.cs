using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MilliMonsterStateMachine : MonsterStateMachine
{
	public Dictionary<eMonsterState, MonsterState> stateDict = new Dictionary<eMonsterState, MonsterState>();
	public MonsterState cState;
	private void Awake()
	{
		Setting();
	}
	private void Setting()
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
	public void ChangeState(eMonsterState stateType)
	{
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}
	private void FixedUpdate()
	{
		cState.Tick();
		delayTime += Time.deltaTime;
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
