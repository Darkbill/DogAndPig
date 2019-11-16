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
    public GameObject charge;
    public ParticleSystem system;
    private float size;
    private bool DragStart = false;
    private float maxChargeTime;
    private float chargeSpeed;

    public void Setting(int id, float dmg, float maxtime, float chargespeed)
    {
        Id = id;
        this.damage = dmg;
        maxChargeTime = maxtime;
        chargeSpeed = chargespeed;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void SystemSetting(Vector3 pos)
    {
        SubSystemSetting(false);
        if (size < 1.0f) size = 1.0f;
        foreach (GameObject obj in effectObjlist)
            obj.transform.localScale = Vector3.one * size;

        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.transform.position = pos;
        system.Play();
    }

    public void SubSystemSetting(bool drag)
    {
        gameObject.SetActive(true);
        system.Stop();
        DragStart = drag;
        if (drag)
            charge.GetComponent<ParticleSystem>().Play();
        else
            charge.GetComponent<ParticleSystem>().Stop();
    }

    private void ExplosionCharge()
    {
        if (DragStart)
        {
            if (size < maxChargeTime)
                size += Time.deltaTime * chargeSpeed;
            charge.transform.position = GameMng.Ins.player.transform.position;
        }
    }

    private void Update()
    {
        ExplosionCharge();
        if (!system.isPlaying && !DragStart)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
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
