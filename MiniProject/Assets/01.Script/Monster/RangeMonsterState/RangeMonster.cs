using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : Monster
{
	public override void Dead()
	{
		base.Dead();
		monsterStateMachine.ChangeStateDead();
	}
}