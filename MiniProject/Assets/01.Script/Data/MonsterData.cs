using GlobalDefine;
public class MonsterData : TableBase
{
	public int monsterID;
	public string monsterName;
	public float size;
	public eMonsterMoveType moveType;
	public float moveSpeed;
	public float rotationSpeed;
	public eMonsterAttackType attackType;
	public float attackSpeed;
	public float attackRange;
	public float attackAngle;
	public float healthPoint;
	public float damage;
	public float armor;
	public float physicsResist;
	public float fireResist;
	public float waterResist;
	public float windResist;
	public float lightningResist;
	public float skillIndex;
	public int level;
	public MonsterData()
	{

	}
	public override float GetTableID()
	{
		return monsterID;
	}
	public float GetResist(eAttackType attackType)
	{
		switch (attackType)
		{
			case eAttackType.Physics:
				return physicsResist;
			case eAttackType.Fire:
				return fireResist;
			case eAttackType.Water:
				return waterResist;
			case eAttackType.Lightning:
				return lightningResist;
			default:
				return 0;
		}
	}
	public void SetMonsterData(int level)
	{
		damage *= level;
		armor *= level;
	}
}
