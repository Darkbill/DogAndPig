using GlobalDefine;
using UnityEngine;
public class MeleeMonster : Monster
{
	public void AttackCheckEnd()
	{
		//애니메이션 호출 함수
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (att.BaseAttack(GetForward(), directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
		{
			GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
			GameMng.Ins.HitToEffect(eAttackType.Physics,
				GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size, 0),
				transform.position + new Vector3(0, monsterData.size, 0),
				GameMng.Ins.player.calStat.size);
		}
	}
}
