using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class RangeMonsterStateMachine : MonoBehaviour
{
	public Dictionary<eRangeMonsterState, RangeMonsterState> stateDict = new Dictionary<eRangeMonsterState, RangeMonsterState>();
	private int monsterID = 10;
	public MonsterData monsterData;
	public RangeMonsterState cState;
	private void Awake()
	{
		Setting();
	}
	private void Setting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[monsterID];
		stateDict.Add(eRangeMonsterState.Idle, new RangeMonsterStateIdle(this));
		stateDict.Add(eRangeMonsterState.Move, new RangeMonsterStateMove(this));
		stateDict.Add(eRangeMonsterState.Stun, new RangeMonsterStateStun(this));
		stateDict.Add(eRangeMonsterState.SkillAttack, new RangeMonsterStateSkillAttack(this));
		stateDict.Add(eRangeMonsterState.Attack, new RangeMonsterStateAttack(this));
		stateDict.Add(eRangeMonsterState.Dash, new RangeMonsterStateDash(this));
		stateDict.Add(eRangeMonsterState.Dead, new RangeMonsterStateDead(this));
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
