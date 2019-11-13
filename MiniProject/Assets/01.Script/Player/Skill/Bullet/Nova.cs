using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nova : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Water;
    eBuffType bufftype = eBuffType.MoveSlow;
	private int id;
    private float per;
	private float slowPer;
	private float endTime;
    private int effectCnt = 0;

    public List<ParticleSystem> particlesystem;
    public CapsuleCollider2D collider2d;
    public GameObject orbParticle;

    private Vector2 fixedUpSiz = new Vector2(3.0f / 20, 1.3f / 20);

    public void Setting(int _id, float p, float _damage,float _slowPer,float _endTime)
    {
        per = p;
		id = _id;
		damage = _damage;
		slowPer = _slowPer;
		endTime = _endTime;
        orbParticle.transform.parent = GameMng.Ins.skillMng.transform;
    }

    public void SystemSetting()
    {
        effectCnt = 0;
        gameObject.SetActive(true);
        collider2d.size = new Vector2(0, 0);
        orbParticle.SetActive(true);
        orbParticle.GetComponent<ParticleSystem>().Simulate(0.1f);
        orbParticle.GetComponent<ParticleSystem>().Play();
    }
    private void FixedUpdate()
    {
        if(effectCnt >= 5)
        {
            gameObject.SetActive(false);
            orbParticle.SetActive(false);
            return;
        }
		gameObject.transform.position = GameMng.Ins.player.transform.position;
        orbParticle.transform.position = GameMng.Ins.player.transform.position +
                                        new Vector3(0, GameMng.Ins.player.calStat.size * 3);
        collider2d.size += fixedUpSiz;
        if (!particlesystem[1].isPlaying)
        {
            for(int i = 0;i<particlesystem.Count;++i)
                particlesystem[i].Play();
            collider2d.size = new Vector2(0, 0);
            ++effectCnt;
        }
    }

	public override void Crash(Monster monster)
	{
        monster.Damage(Attacktype, GameMng.Ins.player.calStat.damage,damage, new ConditionData(bufftype, id, endTime, slowPer),per);
		GameMng.Ins.HitToEffect(Attacktype, 
            monster.transform.position + new Vector3(0, monster.monsterData.size), 
            gameObject.transform.position,
            monster.monsterData.size);
	}
}
