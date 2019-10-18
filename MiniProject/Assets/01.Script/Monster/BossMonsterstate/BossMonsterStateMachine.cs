using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateMachine : MonoBehaviour
{
    public Dictionary<eBossMonsterState, BossMonsterState> stateDict = new Dictionary<eBossMonsterState, BossMonsterState>();
    public BossMonsterState cState;
    private void Awake()
    {
        Setting();
    }
    private void Setting()
    {
        BossMonster o = gameObject.GetComponent<BossMonster>();
        stateDict.Add(eBossMonsterState.Idle, new BossMonsterStateIdle(o));
        stateDict.Add(eBossMonsterState.Move, new BossMonsterStateMove(o));
        //stateDict.Add(eBossMonsterState.Stun, new MilliMonsterStateStun(o));
        stateDict.Add(eBossMonsterState.SkillAttack, new BossMonsterStateSkillAttack(o));
        stateDict.Add(eBossMonsterState.Dash, new BossMonsterStateDead(o));
        stateDict.Add(eBossMonsterState.Dead, new BossMonsterStateDead(o));
        //stateDict.Add(eBossMonsterState.KnockBack, new (o));
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
}
