using GlobalDefine;
public static class ExtensionMethod
{
	public static PlayerData AddStat(this PlayerData levelStat,PlayerData skillStat,ConditionData[] conditionArr)
	{
		PlayerData stat = new PlayerData();
		stat.level = levelStat.level;
		stat.size = levelStat.size;
		stat.healthPoint = levelStat.healthPoint + skillStat.healthPoint;
		stat.damage = levelStat.damage + skillStat.damage + conditionArr[(int)eBuffType.PhysicsStrong].changeValue - conditionArr[(int)eBuffType.PhysicsWeek].changeValue;
		stat.attackRange = levelStat.attackRange + skillStat.attackRange;
		stat.attackAngle = levelStat.attackAngle + skillStat.attackAngle;
		stat.attackSpeed = levelStat.attackSpeed + skillStat.attackSpeed;
		stat.moveSpeed = levelStat.moveSpeed + skillStat.moveSpeed + conditionArr[(int)eBuffType.MoveFast].changeValue - conditionArr[(int)eBuffType.MoveSlow].changeValue;
		stat.criticalChance = levelStat.criticalChance + skillStat.criticalChance;
		stat.criticalDamage = levelStat.criticalDamage + skillStat.criticalDamage;
		stat.armor = levelStat.armor + skillStat.armor;
		stat.physicsResist = levelStat.physicsResist + skillStat.physicsResist;
		stat.fireResist = levelStat.fireResist + skillStat.fireResist;
		stat.windResist = levelStat.windResist + skillStat.windResist;
		stat.waterResist = levelStat.waterResist + skillStat.waterResist;
		stat.lightningResist = levelStat.lightningResist + skillStat.lightningResist;
		return stat;
	}
	public static float CalculatorDamage(this float resist)
	{
		if (resist >= 500)
		{
			return ((1500 - resist) * 0.001f) * 1.6f - 0.6f;
		}
		else
		{
			return ((500 - resist) * 0.001f) * 8f - 1f;
		}
	}
	public static bool GetBuff(this float resist)
	{
		if (resist >= 500)
		{
			float pro = ((1500 - resist) * 0.001f) * 1.6f - 0.6f;
			return Rand.Percent(pro);
		}
		else
		{
			float pro = ((500 - resist) * 0.001f) * 8f - 1f;
			return Rand.Percent(pro);
		}
	}
}