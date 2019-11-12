using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public abstract class StateMachine : MonoBehaviour
{
	public Dictionary<eMonsterState, MonsterStateBase> stateDict = new Dictionary<eMonsterState, MonsterStateBase>();
	public MonsterStateBase cState;
	public Vector3 knockBackDir;
	public float knockBackPower;
	public abstract void ChangeStateKnockBack();
	public virtual void ChangeStateKnockBack(Vector3 _knockBackDir, float _knockBackPower)
	{
		knockBackDir = new Vector3(_knockBackDir.x, _knockBackDir.y, 0);
		knockBackPower = _knockBackPower;
	}
	public abstract void ChangeStateStun();
	public abstract void ChangeStateAttack();
	public abstract void ChangeStateDead();
	public virtual void ChangeStateIdle()
	{
		ChangeState(eMonsterState.Idle);
		UpdateState();
	}
	public virtual void ChangeStateMove()
	{
		ChangeState(eMonsterState.Move);
	}
	public abstract void ChangeStateDamage();
	public abstract void Setting();
	private void Awake()
	{
		Setting();
	}
	private void FixedUpdate()
	{
		cState.Tick();
		if(cState.GetType() == typeof(MonsterStateIdle))
		{
			UpdateState();
		}
	}
	public abstract void UpdateState();

	public virtual void ChangeState(eMonsterState stateType)
	{
		if (cState.monsterObject.active == false) return;
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}
}
