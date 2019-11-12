using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public abstract class StateMachine : MonoBehaviour
{
	public Dictionary<eMonsterState, MonsterStateBase> stateDict = new Dictionary<eMonsterState, MonsterStateBase>();
	public MonsterStateBase cState;
	public Vector3 knockBackDir;
	public float knockBackPower;
	public virtual void ChangeStateKnockBack(Vector3 _knockBackDir, float _knockBackPower)
	{
		knockBackDir = new Vector3(_knockBackDir.x, _knockBackDir.y, 0);
		knockBackPower = _knockBackPower;
		ChangeState(eMonsterState.KnockBack);
	}
	public virtual void ChangeStateStun()
	{
		ChangeState(eMonsterState.Stun);
	}
	public virtual void ChangeStateAttack()
	{
		ChangeState(eMonsterState.Attack);
	}
	public virtual void ChangeStateDead()
	{
		ChangeState(eMonsterState.Dead);
	}
	public virtual void ChangeStateIdle()
	{
		ChangeState(eMonsterState.Idle);
		UpdateState();
	}
	public virtual void ChangeStateMove()
	{
		ChangeState(eMonsterState.Move);
	}
	public void ChangeState(eMonsterState stateType)
	{
		if (cState.monsterObject.active == false) return;
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}
	
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
	public abstract void Setting();
	public abstract void UpdateState();


}
