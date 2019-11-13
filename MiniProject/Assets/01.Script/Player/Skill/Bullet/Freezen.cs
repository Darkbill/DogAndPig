using GlobalDefine;
using UnityEngine;

public class Freezen : BulletPlayerSkill
{
    public float damage = 0;
	eBuffType buffType = eBuffType.MoveSlow;
	eAttackType attackType = eAttackType.Water;
    private int Id = 0;
    private float MaxTimer = 10.0f;
    private float slow = 0.0f;
	private float per;

    private Vector3 rightvec;
    public ParticleSystem system;

	public void Setting(int id,float mT,float s,float p,float d)
	{
		Id = id;
		MaxTimer = mT;
		slow = s;
		per = p;
		damage = d;
		gameObject.SetActive(false);
	}

    private void Update()
    {
        if (system.time > 1.0f)
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if(!system.isPlaying)
            gameObject.SetActive(false);
    }

    public void angleSet(float angle,Vector3 pos)
    {
        rightvec = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
		gameObject.transform.position = pos;
		gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
		gameObject.SetActive(true);
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