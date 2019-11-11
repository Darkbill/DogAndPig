using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDarkness : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Fire;
    eBuffType bufftype = eBuffType.NockBack;

    private int Id = 12;
    public List<GameObject> effectObjlist = new List<GameObject>();
    public ParticleSystem system;
    private float size;

    public void Setting(int id, float siz, float dmg)
    {
        Id = id;
        size = siz;
        foreach (GameObject obj in effectObjlist)
            obj.transform.localScale = Vector3.one * size;
        this.damage = dmg;
        system.Play();
    }

    private void Update()
    {
        if (!system.isPlaying)
            gameObject.SetActive(false);
    }

    public override void Crash(Monster monster)
    {
        monster.Damage(Attacktype, damage);
        GameMng.Ins.HitToEffect(Attacktype,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position,
            monster.monsterData.size);
        monster.OutStateAdd(new ConditionData(bufftype, Id, 0, size * 1.2f), 
            monster.transform.position - transform.position);
    }
}
