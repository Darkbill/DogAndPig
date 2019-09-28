using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MonsterStateMachine : MonoBehaviour
{
	public Dictionary<eMonsterState, MonsterState> stateDict = new Dictionary<eMonsterState, MonsterState>();
	public MonsterState cState;
	private void Awake()
	{
		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(this));
		stateDict.Add(eMonsterState.Chase, new MonsterStateChase(this));
		stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(this));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(this));
		cState = stateDict[eMonsterState.Chase];
		cState.OnStart();
	}
	public void ChangeState(eMonsterState stateType)
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
