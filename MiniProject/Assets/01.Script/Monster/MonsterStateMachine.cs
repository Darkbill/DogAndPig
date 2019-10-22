using UnityEngine;
public abstract class MonsterStateMachine : MonoBehaviour
{
	public Monster monster;
	public abstract void ChangeStateKnockBack();
	public abstract void ChangeStateStun();
	public abstract void ChangeStateAttack();
	public abstract void ChangeStateDead();
	public abstract void ChangeStateIdle();
	public abstract void ChangeStateMove();
	public abstract void ChangeStateDamage();

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
