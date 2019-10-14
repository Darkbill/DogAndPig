using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MilliMonsterStateMachine : MonoBehaviour
{
	public Dictionary<eMilliMonsterState, MilliMonsterState> stateDict = new Dictionary<eMilliMonsterState, MilliMonsterState>();
	public MilliMonsterState cState;
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
	}
}
