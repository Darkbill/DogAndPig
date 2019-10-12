using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public static class ExtensionMethod
{
	public static PlayerData AddStat(this PlayerData levelStat,PlayerData skillStat)
	{
		PlayerData stat = new PlayerData();
		stat.level = levelStat.level;
		stat.size = levelStat.size;
		stat.healthPoint = levelStat.healthPoint + skillStat.healthPoint;
		stat.damage = levelStat.damage + skillStat.damage;
		stat.moveSpeed = levelStat.moveSpeed + skillStat.moveSpeed;
		stat.attackRange = levelStat.attackRange + skillStat.attackRange;
		stat.attackAngle = levelStat.attackAngle + skillStat.attackRange;
		stat.attackSpeed = levelStat.attackSpeed + skillStat.attackSpeed;
		stat.moveSpeed = levelStat.moveSpeed + skillStat.moveSpeed;
		stat.criticalChange = levelStat.criticalChange + skillStat.criticalChange;
		stat.criticalDamage = levelStat.criticalDamage + skillStat.criticalDamage;
		stat.armor = levelStat.armor + skillStat.armor;
		stat.physicsResist = levelStat.physicsResist + skillStat.physicsResist;
		stat.fireResist = levelStat.fireResist + skillStat.fireResist;
		stat.windResist = levelStat.windResist + skillStat.windResist;
		stat.waterResist = levelStat.waterResist + skillStat.waterResist;
		stat.lightningResist = levelStat.lightningResist + skillStat.lightningResist;
		return stat;
	}
}