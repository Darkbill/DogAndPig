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
	public abstract void ChangeStateIdle();
	public abstract void ChangeStateMove();
	public abstract void ChangeStateDamage();
	public abstract void Setting();
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
	public float delayTime = 0;
	public bool IsAttack()
	{
		if (delayTime >= monster.monsterData.attackSpeed)
		{
			return true;
		}
		return false;
	}
}
