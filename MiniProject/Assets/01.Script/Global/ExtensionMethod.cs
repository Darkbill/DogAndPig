using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
	public static PlayerData AddStat(this PlayerData levelStat,PlayerData skillStat)
	{
		PlayerData stat = new PlayerData();
		stat.level = levelStat.level;
		stat.healthPoint = levelStat.healthPoint + skillStat.healthPoint;
		stat.damage = levelStat.damage + skillStat.damage;
		stat.moveSpeed = levelStat.moveSpeed + skillStat.moveSpeed;
		stat.criticalChange = levelStat.criticalChange + skillStat.criticalChange;
		stat.criticalDamage = levelStat.criticalDamage + skillStat.criticalDamage;
		stat.armor = levelStat.armor + skillStat.armor;
		return stat;
	}
}
