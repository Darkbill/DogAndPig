using System.Collections.Generic;
using GlobalDefine;
public class RangeMonsterStateMachine : MonsterStateMachine
{
	public Dictionary<eMonsterState, MonsterState> stateDict = new Dictionary<eMonsterState, MonsterState>();
	public MonsterState cState;
	private void Awake()
	{
		Setting();
	}
	private void Setting()
	{
		RangeMonster o = gameObject.GetComponent<RangeMonster>();

		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(o));
		stateDict.Add(eMonsterState.Move, new RangeMonsterStateMove(o));
		stateDict.Add(eMonsterState.SkillAttack, new RangeMonsterStateSkillAttack(o));
		stateDict.Add(eMonsterState.Attack, new RangeMonsterStateAttack(o));

		stateDict.Add(eMonsterState.Damage, new MonsterStateDamage(o));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(o));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(o));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(o));
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
