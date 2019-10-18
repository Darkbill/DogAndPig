using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilliMonsterStateAttack : MonsterState
{
	public MilliMonsterStateAttack(MilliMonster o) : base(o)
	{

	}
	public override void OnStart()
	{

	}

	public override bool OnTransition()
	{
		return true;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{

	}
}
