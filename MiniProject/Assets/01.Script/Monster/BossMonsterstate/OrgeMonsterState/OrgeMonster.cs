﻿using UnityEngine;
using GlobalDefine;
public class OrgeMonster : Monster
{
	public Vector3 toPlayerDir;
	public bool skillFlag;
	public void AttackCheckEnd()
	{
		//애니메이션 호출 함수
		if(CheckDistance())
		{
			GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
		}
	}
	public void SkillCheckEnd()
	{
		//애니메이션 호출 함수
		if (CheckDistance())
		{
			GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage * 2);
		}
	}
	private bool CheckDistance()
	{
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (att.BaseAttack(GetForward(), directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
		{
			GameMng.Ins.HitToEffect(eAttackType.Physics,
				GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size, 0),
				transform.position + new Vector3(0, monsterData.size, 0),
				GameMng.Ins.player.calStat.size);
			return true;
		}
		return false;
	}
	public override void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.DamageToBoss(d, transform.position);
		if (monsterData.healthPoint <= 0) Dead();
	}
	public void AllClear()
	{
		GameMng.Ins.WorldClear();
	}
}