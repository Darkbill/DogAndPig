using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MilliMonster : MonoBehaviour
{
	public MilliMonsterStateMachine monsterStateMachine;
	public MonsterData monsterData;

	/* 테스트코드 */
	private int MonsterID = 1;
	public void Start()
	{
		MonsterSetting();
	}
	public void MonsterSetting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[MonsterID];
	}
	/* 테스트코드 */
	public void Attack()
	{
		GameMng.Ins.DamageToPlayer(eAttackType.Physics,monsterData.damage);
		StartCoroutine(AttackAnimationDelay());
	}
	IEnumerator AttackAnimationDelay()
	{
		Debug.Log("몬스터 공격");
		yield return new WaitForSeconds(1);
		monsterStateMachine.ChangeState(eMilliMonsterState.Move);
	}
	public void Hit()
	{
		Debug.Log("몬스터가 공격을 당했습니다.");
	}
}
