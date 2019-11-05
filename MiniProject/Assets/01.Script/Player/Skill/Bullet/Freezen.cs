using GlobalDefine;

public class Freezen : BulletPlayerSkill
{
    public float damage = 0;
	eBuffType buffType;
	eAttackType attackType;
    private int Id = 0;
    private float MaxTimer = 10.0f;
    private float slow = 0.0f;
	private float per;

	public void Setting(int id,float mT,float s,float p,float d,eAttackType aType,eBuffType bType)
	{
		Id = id;
		MaxTimer = mT;
		slow = s;
		per = p;
		damage = d;
		attackType = aType;
		buffType = bType;
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(eAttackType.Water, GameMng.Ins.player.calStat.damage, damage, new ConditionData(buffType, Id, MaxTimer, slow), per);
		GameMng.Ins.HitToEffect(attackType, monster.transform.position, gameObject.transform.position);
	}
}