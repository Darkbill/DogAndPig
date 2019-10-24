using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateMachine : MonsterStateMachine
{
    public Dictionary<eMonsterState, MonsterState> stateDict = new Dictionary<eMonsterState, MonsterState>();
    public MonsterState cState;
    private void Awake()
    {
        Setting();
    }
    private void Setting()
    {
        BossMonster o = gameObject.GetComponent<BossMonster>();
        o.BossSkill01 = Instantiate(Resources.Load(string.Format("Skill_FireBullet"), 
            typeof(SkillBurningMeteor)) as SkillBurningMeteor);

        stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(o));
        stateDict.Add(eMonsterState.Move, new BossMonsterStateMove(o));
        stateDict.Add(eMonsterState.SkillAttack, new BossMonsterStateSkillAttack(o));
        stateDict.Add(eMonsterState.Attack, new BossMonsterStateAttack(o));

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
		return;
	}
	public override void ChangeStateStun()
	{
		return;
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
		return;
	}
}
