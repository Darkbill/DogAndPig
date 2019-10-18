using UnityEngine;

public abstract class MonsterStateMachine : MonoBehaviour
{
	public abstract void ChangeStateKnockBack();
	public abstract void ChangeStateStun();
	public abstract void ChangeStateAttack();
	public abstract void ChangeStateDead();
	public abstract void ChangeStateIdle();
	public abstract void ChangeStateMove();

}
