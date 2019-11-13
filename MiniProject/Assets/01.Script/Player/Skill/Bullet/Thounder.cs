using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;

public class Thounder : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Lightning;
    public bool hit = false;

    public List<ParticleSystem> particle = new List<ParticleSystem>();

    public void Setting(float dmg)
    {
        damage = dmg;
        hit = false;
		gameObject.SetActive(false);
    }

    public void SettingSystem(Vector3 pos) 
    {
        pos.z = 0;
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
        foreach (ParticleSystem p in particle)
            p.Play();
    }

    private void Update()
    {
        if (!particle[0].isPlaying)
            gameObject.SetActive(false);
    }

    public override void Crash(Monster monster)
    {
        hit = true;
		if (monster.active == false) return;
		monster.Damage(Attacktype,GameMng.Ins.player.calStat.damage, damage);
        GameMng.Ins.HitToEffect(Attacktype,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position,
            monster.monsterData.size);
    }
}
