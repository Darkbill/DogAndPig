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
	public void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - monsterData.armor);

		switch (attackType)
		{
			case eAttackType.Physics:
				d -= (int)(((1000 - monsterData.physicsResist) * 0.001) + 0.5);
				break;
			case eAttackType.Fire:
				d -= (int)(((1000 - monsterData.fireResist) * 0.001) + 0.5);
				break;
			case eAttackType.Water:
				d -= (int)(((1000 - monsterData.waterResist) * 0.001) + 0.5);
				break;
			case eAttackType.Wind:
				d -= (int)(((1000 - monsterData.windResist) * 0.001) + 0.5);
				break;
			case eAttackType.Lightning:
				d -= (int)(((1000 - monsterData.lightningResist) * 0.001) + 0.5);
				break;
		}
		if (d < 1) d = 1;
		//TODO : 몬스터체력
		monsterData.healthPoint -= (int)d;
		UIMngInGame.Ins.ShowDamage((int)d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
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
