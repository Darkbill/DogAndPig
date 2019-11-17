using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindKnockback : BulletPlayerSkill
{
    public ParticleSystem knockbackParticle;
    //처음 1회만 넉벡적용을 위해 collider 없앰
    public ParticleSystem bufParticle;

    private eAttackType type = eAttackType.Wind;
    private eBuffType debuftype = eBuffType.NockBack;
    private eBuffType bufType = eBuffType.MoveFast;

    private int skillID;
    private float bufTime;
    private float setTime = 0f;
    private float radius;
    private float speedPer;

    public void Setting(int _skillID, float _buffTime, float _radius, float _speedper)
    {
        skillID = _skillID;
        bufTime = _buffTime;
        radius = _radius;
        speedPer = _speedper;
        gameObject.transform.parent = GameMng.Ins.skillMng.transform;
        gameObject.SetActive(false);
    }

    public void SystemSetting()
    {
        gameObject.SetActive(true);
        knockbackParticle.Play();
        knockbackParticle.Play();
        bufParticle.Play();
        setTime = 0.0f;
        CrashKnockback();
        FastBuff();
    }

    public void OffSkill()
    {
        knockbackParticle.Stop();
        bufParticle.Stop();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        gameObject.transform.position = GameMng.Ins.player.transform.position;
        setTime += Time.deltaTime;
        if (setTime > bufTime)
            gameObject.SetActive(false);
    }


    private void CrashKnockback()
    {
        var hitMonsterList = GameMng.Ins.monsterPool.monsterList;
        foreach(Monster m in hitMonsterList)
        {
            if (m == null || !m.active || !m.gameObject.activeSelf) continue;
            if ((m.transform.position - GameMng.Ins.player.transform.position).magnitude < radius && m.gameObject.activeSelf)
                m.OutStateAdd(new ConditionData(debuftype, skillID, 0, radius), 
                    m.transform.position - GameMng.Ins.player.transform.position);
        }
    }

    private void FastBuff()
    {
        GameMng.Ins.player.AddBuff(new ConditionData(bufType, skillID, bufTime, speedPer));
    }

    public override void Crash(Monster monster)
    {
        //if (hitMonsterList.Contains(monster)) return;
        //hitMonsterList.Add(monster);
        //monster.OutStateAdd(new ConditionData(eBuffType.NockBack, skillID, 0, knockBackPower), monster.transform.position - transform.position);
    }
}
