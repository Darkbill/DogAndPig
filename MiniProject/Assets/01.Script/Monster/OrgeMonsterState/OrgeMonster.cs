using UnityEngine;
using GlobalDefine;
public class OrgeMonster : Monster
{
	public Vector3 toPlayerDir;
	public bool skillFlag;
	public void AttackCheckEnd()
	{
		//애니메이션 호출 함수
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (att.BaseAttack(GetForward(), directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
		{
			if (skillFlag)
			{
				GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage * 2);
				skillFlag = false;
			}
			else
			{
				GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
			}
			GameMng.Ins.HitToEffect(eAttackType.Physics,
				GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size, 0),
				transform.position + new Vector3(0, monsterData.size, 0),
				GameMng.Ins.player.calStat.size);
		}
	}
}
