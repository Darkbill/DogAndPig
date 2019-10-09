using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MonsterStateMachine : MonoBehaviour
{
	public Dictionary<eMonsterState, MonsterState> stateDict = new Dictionary<eMonsterState, MonsterState>();
	private int monsterID = 1;
	public MonsterData monsterData;
	public MonsterState cState;
	private void Awake()
	{
		Setting();
	}
	private void Setting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[monsterID];
		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(this));
		stateDict.Add(eMonsterState.Chase, new MonsterStateChase(this));
		stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(this));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(this));
		stateDict.Add(eMonsterState.Dodge, new MonsterStateDodge(this));
		cState = stateDict[eMonsterState.Idle];
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
