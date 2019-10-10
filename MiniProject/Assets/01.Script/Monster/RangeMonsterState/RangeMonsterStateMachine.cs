using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class RangeMonsterStateMachine : MonoBehaviour
{
	public Dictionary<eRangeMonsterState, RangeMonsterState> stateDict = new Dictionary<eRangeMonsterState, RangeMonsterState>();
	public RangeMonsterState cState;
	private void Awake()
	{
		Setting();
	}
	private void Setting()
	{
		RangeMonster o = gameObject.GetComponent<RangeMonster>();
		stateDict.Add(eRangeMonsterState.Idle, new RangeMonsterStateIdle(o));
		stateDict.Add(eRangeMonsterState.Move, new RangeMonsterStateMove(o));
		stateDict.Add(eRangeMonsterState.Stun, new RangeMonsterStateStun(o));
		stateDict.Add(eRangeMonsterState.SkillAttack, new RangeMonsterStateSkillAttack(o));
		stateDict.Add(eRangeMonsterState.Attack, new RangeMonsterStateAttack(o));
		stateDict.Add(eRangeMonsterState.Dash, new RangeMonsterStateDash(o));
		stateDict.Add(eRangeMonsterState.Dead, new RangeMonsterStateDead(o));
		cState = stateDict[eRangeMonsterState.Idle];
		cState.OnStart();
	}
	public void ChangeState(eRangeMonsterState stateType)
	{
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}
	private void Update()
	{
		cState.Tick();
	}
}
