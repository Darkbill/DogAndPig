using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MilliMonsterStateMachine : MonsterStateMachine
{
	public Dictionary<eMilliMonsterState, MonsterState> stateDict = new Dictionary<eMilliMonsterState, MonsterState>();
	public MonsterState cState;
	private void Awake()
	{
		Setting();
	}
	private void Setting()
	{
		MilliMonster o = gameObject.GetComponent<MilliMonster>();
		stateDict.Add(eMilliMonsterState.Idle, new MilliMonsterStateIdle(o));
		stateDict.Add(eMilliMonsterState.Move, new MilliMonsterStateMove(o));
		stateDict.Add(eMilliMonsterState.Stun, new MilliMonsterStateStun(o));
		stateDict.Add(eMilliMonsterState.SkillAttack, new MilliMonsterStateSkillAttack(o));
		stateDict.Add(eMilliMonsterState.Dash, new MilliMonsterStateDash(o));
		stateDict.Add(eMilliMonsterState.Dead, new MilliMonsterStateDead(o));
		stateDict.Add(eMilliMonsterState.KnockBack, new MilliMonsterStateKnockBack(o));
        cState = stateDict[eMilliMonsterState.Idle];
		cState.OnStart();
	}
	public void ChangeState(eMilliMonsterState stateType)
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
		ChangeState(eMilliMonsterState.KnockBack);
	}
	public override void ChangeStateStun()
	{
		ChangeState(eMilliMonsterState.Stun);
	}
	public override void ChangeStateAttack()
	{
		//ChangeState(eMilliMonsterState.SkillAttack);
	}
	public override void ChangeStateDead()
	{
		ChangeState(eMilliMonsterState.Dead);
	}
	public override void ChangeStateIdle()
	{
		ChangeState(eMilliMonsterState.Idle);
		cState.monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
	}
	public override void ChangeStateMove()
	{
		ChangeState(eMilliMonsterState.Move);
	}
}
