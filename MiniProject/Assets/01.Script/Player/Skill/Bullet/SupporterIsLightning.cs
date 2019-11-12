using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupporterIsLightning : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Lightning;
    eBuffType bufftype = eBuffType.Stun;

    private int Id;
    private float MaxTimer = 0.0f;

    public float Speed;

    private float setTimer = 0.0f;

    public LightningBoltIsSupport lightningbase;

    List<LightningBoltIsSupport> lightningList = new List<LightningBoltIsSupport>();
    List<Monster> monsterlist = new List<Monster>();

    public void Setting(int id, float _damage, float _speed, float _EndTime)
    {
        Id = id;
        MaxTimer = _EndTime;
        Speed = _speed;
        damage = _damage;
        setTimer = 0.0f;
    }
    public void SetPosition(Vector3 pos) { pos.z = 0; gameObject.transform.position = pos; }

    public void StartSupporterAttack(){ StartCoroutine(MonsterLightningAttack()); }

    private void Update()
    {
        setTimer += Time.deltaTime;
        if (MaxTimer <= setTimer)
            gameObject.SetActive(false);
    }

    private IEnumerator MonsterLightningAttack()
    {
        while (MaxTimer > setTimer)
        {
            yield return new WaitForSeconds(Speed);
            SelectMonsterObject();
        }
    }

    private void SelectMonsterObject()
    {
        for(int i = 0;i<monsterlist.Count;++i)
        {
            if ( MonsterDelete(monsterlist[i]) ) { --i; continue; }
            if(lightningList.Count <= i)
            {
                CreateBullet(monsterlist[i].transform.position);
                SetDamage(monsterlist[i]);
                continue;
            }
            lightningList[i].Setting(Id, monsterlist[i].transform.position, gameObject.transform.position);
            lightningList[i].gameObject.SetActive(true);
            SetDamage(monsterlist[i]);
        }
        monsterlist.Clear();
    }

    private bool MonsterDelete(Monster monster)
    {
        if (monster == null) { monsterlist.Remove(monster); return true; }
        return false;
    }

    private void CreateBullet(Vector3 pos)
    {
        LightningBoltIsSupport o = Instantiate(lightningbase, GameMng.Ins.skillMng.transform);
        o.Setting(Id, pos, gameObject.transform.position);
        o.gameObject.SetActive(true);
        lightningList.Add(o);
    }

    private void SetDamage(Monster monster)
    {
        if (monster != null)
        {
            monster.Damage(Attacktype, GameMng.Ins.player.calStat.damage, damage);
            GameMng.Ins.HitToEffect(Attacktype,
                monster.transform.position + new Vector3(0, monster.monsterData.size),
                gameObject.transform.position + new Vector3(-0.3f, -0.15f),
                monster.monsterData.size);
        }
    }

    public override void Crash(Monster monster)
    {
        
    }

    public override void CrashOfIn(Monster monster)
    {
        base.CrashOfIn(monster);
        if (!monsterlist.Contains(monster) && monster != null)
            monsterlist.Add(monster);
    }
}
