using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MilliMonster : Monster
{
	public MilliMonsterStateMachine monsterStateMachine;
	/* 테스트코드 */
	public override void Attack()
	{
		base.Attack();
		StartCoroutine(AttackAnimationDelay());
	}
	IEnumerator AttackAnimationDelay()
	{
		Debug.Log("몬스터 공격");
		yield return new WaitForSeconds(1);
		monsterStateMachine.ChangeState(eMilliMonsterState.Move);
	}
	public override void Dead()
	{
		base.Dead();
		monsterStateMachine.ChangeState(eMilliMonsterState.Dead);
	}
}
