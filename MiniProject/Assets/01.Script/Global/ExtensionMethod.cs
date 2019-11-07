using GlobalDefine;
using System.Collections.Generic;
public static class ExtensionMethod
{
	public static PlayerData AddStat(this PlayerData levelStat,PlayerData skillStat,List<ConditionData> conditionList)
	{
        #region PlayerDataSetting
        PlayerData stat = new PlayerData();
		int level = JsonMng.Ins.playerInfoDataTable.playerLevel;
		var equip = JsonMng.Ins.playerInfoDataTable.equipItemList;
		for (int i = 0; i < equip.Length; ++i)
		{
			if (equip[i] == null) continue;
			switch (equip[i].upgradeType)
			{
				case eUpgradeType.Damage:
					levelStat.damage += levelStat.damage * equip[i].changeValue;
					break;
				case eUpgradeType.Armor:
					levelStat.armor += levelStat.armor * equip[i].changeValue;
					break;
				case eUpgradeType.CriticalChance:
					levelStat.criticalChance += levelStat.criticalChance * equip[i].changeValue;
					break;
				case eUpgradeType.CriticalDamage:
					levelStat.criticalDamage += levelStat.criticalDamage * equip[i].changeValue;
					break;
				case eUpgradeType.FireResist:
					levelStat.fireResist += levelStat.fireResist * equip[i].changeValue;
					break;
				case eUpgradeType.HP:
					levelStat.healthPoint += levelStat.healthPoint * equip[i].changeValue;
					break;
				case eUpgradeType.KnockBack:
					levelStat.knockback += levelStat.knockback * equip[i].changeValue;
					break;
				case eUpgradeType.PhysicsResist:
					levelStat.physicsResist += levelStat.physicsResist * equip[i].changeValue;
					break;
				case eUpgradeType.Speed:
					levelStat.moveSpeed += levelStat.moveSpeed * equip[i].changeValue;
					break;
				case eUpgradeType.WaterResist:
					levelStat.waterResist += levelStat.waterResist * equip[i].changeValue;
					break;
				case eUpgradeType.WindResist:
					levelStat.windResist += levelStat.windResist * equip[i].changeValue;
					break;
			}
		}
		stat.size = levelStat.size;
		stat.healthPoint = levelStat.GetHealthPoint(level) + skillStat.healthPoint;
		stat.damage = (levelStat.damage * level) + skillStat.damage;
		stat.attackRange = levelStat.attackRange + skillStat.attackRange;
		stat.attackAngle = levelStat.attackAngle + skillStat.attackAngle;
		stat.attackSpeed = levelStat.attackSpeed + skillStat.attackSpeed;
		stat.moveSpeed = levelStat.moveSpeed + skillStat.moveSpeed;
		stat.criticalChance = levelStat.criticalChance + skillStat.criticalChance;
		stat.criticalDamage = levelStat.criticalDamage + skillStat.criticalDamage;
		stat.armor = (levelStat.armor * level) + skillStat.armor;
		stat.physicsResist = levelStat.physicsResist + skillStat.physicsResist;
		stat.fireResist = levelStat.fireResist + skillStat.fireResist;
		stat.windResist = levelStat.windResist + skillStat.windResist;
		stat.waterResist = levelStat.waterResist + skillStat.waterResist;
		stat.lightningResist = levelStat.lightningResist + skillStat.lightningResist;


        #endregion

        float stunMaxtime = 0;

        for (int i = 0; i < conditionList.Count; ++i)
		{
			switch(conditionList[i].buffType)
			{
				case eBuffType.MoveFast:
					stat.moveSpeed += conditionList[i].changeValue;
					break;
				case eBuffType.MoveSlow:
					stat.moveSpeed -= conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsStrong:
					stat.damage += conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsWeek:
					stat.damage -= conditionList[i].changeValue;
					break;
                case eBuffType.NockBack:
                    stat.knockback = conditionList[i].changeValue;
                    break;
                case eBuffType.Stun:
                    if (stunMaxtime < conditionList[i].currentTime)
                    {
                        stunMaxtime = conditionList[i].currentTime;
                        stat.stun = conditionList[i].changeValue;
                    }
                    break;
			}
		}
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
	public static bool GetBuff(this float resist,float activePer)
	{
		if (resist >= 500)
		{
			float pro = ((1500 - resist) * 0.001f) * 1.6f - 0.6f;
			return Rand.Percent((activePer*pro)/10);
		}
		else
		{
			float pro = ((500 - resist) * 0.001f) * 8f - 1f;
			return Rand.Percent((activePer * pro) / 10);
		}
	}
	public static int FindID(this List<ConditionData> conditionDataList,int id, eBuffType type = eBuffType.None)
	{
        if(type == eBuffType.None)
        {
            for (int i = 0; i < conditionDataList.Count; ++i)
            {
                if (conditionDataList[i].skillIndex == id)
                {
                    return i;
                }
            }
            return -1;
        }
		for (int i = 0; i < conditionDataList.Count; ++i)
		{
			if (conditionDataList[i].skillIndex == id &&
                conditionDataList[i].buffType == type)
			{
				return i;
			}
		}
		return -1;
	}
	public static MonsterData Copy(this MonsterData original)
	{
		MonsterData m = new MonsterData();
		m.monsterID = original.monsterID;
		m.monsterName = original.monsterName;
		m.size = original.size;
		m.moveType = original.moveType;
		m.moveSpeed = original.moveSpeed;
		m.rotationSpeed = original.rotationSpeed;
		m.attackType = original.attackType;
		m.attackSpeed = original.attackSpeed;
		m.attackRange = original.attackRange;
		m.attackAngle = original.attackAngle;
		m.healthPoint = original.healthPoint;
		m.damage = original.damage;
		m.armor = original.armor;
		m.physicsResist = original.physicsResist;
		m.fireResist = original.fireResist;
		m.waterResist = original.waterResist;
		m.windResist = original.windResist;
		m.lightningResist = original.lightningResist;
		m.skillIndex = original.skillIndex;
		return m;
	}
	public static ItemData Copy(this ItemData original)
	{
		ItemData m = new ItemData();
		m.changeOption = original.changeOption;
		m.changeSkill = original.changeSkill;
		m.changeSkillValue = original.changeSkillValue;
		m.changeValue = original.changeValue;
		m.itemGradeType = original.itemGradeType;
		m.itemID = original.itemID;
		m.itemName = original.itemName;
		m.itemType = original.itemType;
		m.upgradeType = original.upgradeType;
		return m;
	}
	public static List<T> ToList<T>(this Dictionary<int, T> dict)
	{
		List<T> list = new List<T>();
		var i = dict.GetEnumerator();
		while (i.MoveNext())
		{
			list.Add(i.Current.Value);
		}
		return list;
	}
}