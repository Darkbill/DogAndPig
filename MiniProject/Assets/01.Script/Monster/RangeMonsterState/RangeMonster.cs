using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : Monster
{
	public MilliMonsterStateMachine monsterStateMachine;
	public override void Dead()
	{
		base.Dead();
		monsterStateMachine.ChangeState(GlobalDefine.eMilliMonsterState.Dead);
	}
}