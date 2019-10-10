using GlobalDefine;
public class MonsterData : TableBase
{
	public float monsterID;
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

	public MonsterData()
	{

	}
	public override float GetTableID()
	{
		return monsterID;
	}
}
