using UnityEngine;
using GlobalDefine;
using System.Collections.Generic;
public abstract class MonsterStateMachine : MonoBehaviour
{
	public Monster monster;
	public Dictionary<eMonsterState, MonsterState> stateDict = new Dictionary<eMonsterState, MonsterState>();
	public MonsterState cState;
	public abstract void ChangeStateKnockBack();
	public abstract void ChangeStateStun();
	public abstract void ChangeStateAttack();
	public abstract void ChangeStateDead();
	public virtual void ChangeStateIdle()
	{
		ChangeState(eMonsterState.Idle);
	}
	public virtual void ChangeStateMove()
	{
		ChangeState(eMonsterState.Move);
	}
	public abstract void ChangeStateDamage();
	public abstract void Setting();
	public float delayTime = 0;
	private void Awake()
	{
		Setting();
	}
	private void FixedUpdate()
	{
		cState.Tick();
		delayTime += Time.deltaTime;
	}
	public virtual void ChangeState(eMonsterState stateType)
	{
		if (monster.active == false) return;
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}

	public bool IsAttack()
	{
		if (delayTime >= Define.standardAttackSpeed / monster.monsterData.attackSpeed)
		{
			return true;
		}
		return false;
	}
}
