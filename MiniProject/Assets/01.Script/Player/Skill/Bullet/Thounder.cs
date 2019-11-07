using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thounder : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Fire;
    eBuffType bufftype = eBuffType.NockBack;

    private int Id = 13;
    public bool hit = false;

    public ParticleSystem particle;

    public void Setting(int id, float siz, float dmg)
    {
        Id = id;
        damage = dmg;
        hit = false;
    }

    public void isPlay() { particle.Play(); }

    private void Update()
    {
        if (!particle.isPlaying)
            gameObject.SetActive(false);
    }

    public override void Crash(Monster monster)
    {
        hit = true;
        monster.Damage(Attacktype, damage);
        GameMng.Ins.HitToEffect(Attacktype,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position,
            monster.monsterData.size);
    }
}
