using GlobalDefine;
using UnityEngine;

public class Freezen : BulletPlayerSkill
{
    public float damage = 0;
	eBuffType buffType;
	eAttackType attackType;
    private int Id = 0;
    private float MaxTimer = 10.0f;
    private float slow = 0.0f;
	private float per;

    private Vector3 rightvec;
    public ParticleSystem system;

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

    private void Update()
    {
        if (system.time > 1.0f)
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if(!system.isPlaying)
            gameObject.SetActive(false);
    }

    public void angleSet(float angle)
    {
        rightvec = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

	public override void Crash(Monster monster)
	{
		monster.Damage(attackType, GameMng.Ins.player.calStat.damage, damage, new ConditionData(buffType, Id, MaxTimer, slow), per);
		GameMng.Ins.HitToEffect(attackType, 
            monster.transform.position + new Vector3(0, monster.monsterData.size), 
            GameMng.Ins.player.transform.position + rightvec,
            monster.monsterData.size);
	}
}