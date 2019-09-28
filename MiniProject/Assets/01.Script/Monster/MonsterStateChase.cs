using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateChase : MonsterState
{
	public MonsterStateChase(MonsterStateMachine o) : base(o)
	{

	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
		return false;
		//if ()
		//{
		//	return true;
		//}
		//return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
		ChaseToPlayer();
	}
	public override void OnEnd()
	{

	}
	public void ChaseToPlayer()
	{
		Vector3 direction = GameMng.Ins.player.transform.position - owner.transform.position;
		owner.gameObject.transform.LookAt(direction);
		owner.gameObject.transform.Translate(owner.transform.forward * Time.deltaTime);
	}
}
