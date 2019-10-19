using UnityEngine;

public abstract class MonsterStateMachine : MonoBehaviour
{
	public abstract void ChangeStateKnockBack();
	public abstract void ChangeStateStun();
	public abstract void ChangeStateAttack();
	public abstract void ChangeStateDead();
	public abstract void ChangeStateIdle();
	public abstract void ChangeStateMove();
	public float delayTime;
	public bool IsAttack()
	{
		if (delayTime >= gameObject.GetComponent<Monster>().monsterData.attackSpeed)
		{
			delayTime = 0.0f;
			return true;
		}
		return false;
	}
}
