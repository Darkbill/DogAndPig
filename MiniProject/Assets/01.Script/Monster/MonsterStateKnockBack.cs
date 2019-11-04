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
		setspeed = Define.knockBackSpeed;
		range = monsterObject.transform.position -
			monsterObject.monsterStateMachine.knockBackDir;
		range.Normalize();
	}

	public override bool OnTransition()
	{
		return false;
	}

	public override void Tick()
	{
		if (setspeed > 0)
			KnockBack();
		if (OnTransition() == true) return;
	}

	private void KnockBack()
	{
		monsterObject.transform.position += range * Time.deltaTime * setspeed;
		--setspeed;
	}

	public override void OnEnd()
	{

	}
}
