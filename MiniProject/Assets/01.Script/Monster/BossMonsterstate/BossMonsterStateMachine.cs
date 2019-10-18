using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateMachine : MonsterStateMachine
{
    public Dictionary<eBossMonsterState, MonsterState> stateDict = new Dictionary<eBossMonsterState, MonsterState>();
    public MonsterState cState;
    private void Awake()
    {
        Setting();
    }
    private void Setting()
    {
        BossMonster o = gameObject.GetComponent<BossMonster>();
        stateDict.Add(eBossMonsterState.Idle, new BossMonsterStateIdle(o));
        stateDict.Add(eBossMonsterState.Move, new BossMonsterStateMove(o));
        stateDict.Add(eBossMonsterState.SkillAttack, new BossMonsterStateSkillAttack(o));
        stateDict.Add(eBossMonsterState.Dash, new BossMonsterStateDead(o));
        stateDict.Add(eBossMonsterState.Dead, new BossMonsterStateDead(o));
        cState = stateDict[eBossMonsterState.Idle];
        cState.OnStart();
    }
    public void ChangeState(eBossMonsterState stateType)
    {
        cState.OnEnd();
        cState = stateDict[stateType];
        cState.OnStart();
    }
    private void FixedUpdate()
    {
        cState.Tick();
    }
	public override void ChangeStateKnockBack()
	{
		return;
	}
	public override void ChangeStateStun()
	{
		return;
	}
	public override void ChangeStateAttack()
	{
		ChangeState(eBossMonsterState.SkillAttack);
	}
	public override void ChangeStateDead()
	{
		ChangeState(eBossMonsterState.Dead);
	}
	public override void ChangeStateIdle()
	{
		ChangeState(eBossMonsterState.Idle);
	}
	public override void ChangeStateMove()
	{
		ChangeState(eBossMonsterState.Move);
	}
}
