using UnityEngine;
using GlobalDefine;
public class MonsterStateKnockBack : MonsterState
{
	private float setspeed;
	private Vector3 range;

	public MonsterStateKnockBack(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
		setspeed = Define.knockBackSpeed * monsterObject.monsterStateMachine.knockBackPower;
		range = monsterObject.monsterStateMachine.knockBackDir;
		range.Normalize();
	}

	public override bool OnTransition()
	{
		if (setspeed <= 0)
		{
			monsterObject.monsterStateMachine.ChangeStateIdle();
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
		KnockBack();
	}

	private void KnockBack()
	{
		monsterObject.transform.position += range * Time.deltaTime * setspeed;
		setspeed -= Time.deltaTime * 100;
	}

	public override void OnEnd()
	{
		setspeed = 0;
	}
}
