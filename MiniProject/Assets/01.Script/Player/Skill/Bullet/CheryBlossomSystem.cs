using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheryBlossomSystem : MonoBehaviour
{
    private List<GameObject> mainParticleobj = new List<GameObject>();
    public GameObject mainparticle;

    private eAttackType Attacktype = eAttackType.Wind;

    private float damage;
    private float delayTime;
    private float setTime;
    private float radius;

    private float firstPlayerHp;
    private int skillId;

    public void Setting(int _skillid, float _damage, float _delayTime, float _radius)
    {
        skillId = _skillid;
        damage = _damage;
        delayTime = _delayTime;
        setTime = 0.0f;
        radius = _radius;
        gameObject.SetActive(false);
        gameObject.transform.parent = GameMng.Ins.skillMng.transform;

        gameObject.transform.localScale *= _radius;
        mainparticle.transform.localScale *= _radius;

        GameObject o = Instantiate(mainparticle, GameMng.Ins.skillMng.transform);
        o.transform.position = GameMng.Ins.player.transform.position;
        o.SetActive(false);
        mainParticleobj.Add(o);
    }
    public void SystemSetting()
    {
        foreach (GameObject p in mainParticleobj)
        {
            if (!p.activeSelf)
            {
                p.SetActive(true);
                p.GetComponent<ParticleSystem>().Play();
                break;
            }
        }
        firstPlayerHp = GameMng.Ins.player.calStat.healthPoint;
        setTime = 0.0f;
        gameObject.SetActive(true);
        GameMng.Ins.player.AddBuff(new ConditionData(eBuffType.MoveFast, skillId, delayTime, 0));
    }
    private void Update()
    {
        PlayerHpCheck();
        ParticleUpdate();
        setTime += Time.deltaTime;
        if (setTime > delayTime)
        {
            gameObject.SetActive(false);
            foreach (GameObject p in mainParticleobj)
                p.GetComponent<ParticleSystem>().Stop();
        }
    }

    private void ParticleUpdate()
    {
        gameObject.transform.position = GameMng.Ins.player.transform.position;
        foreach (GameObject p in mainParticleobj)
        {
            if (!p.GetComponent<ParticleSystem>().isPlaying) { p.SetActive(false); continue; }
            p.transform.position = GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size);
        }
    }

    private void PlayerHpCheck()
    {
        float nowHealthPoint = GameMng.Ins.player.calStat.healthPoint;
        if (firstPlayerHp > nowHealthPoint)
        {
            firstPlayerHp = nowHealthPoint;
            ReflectAttack();
            foreach (GameObject p in mainParticleobj)
            {
                if (!p.activeSelf)
                {
                    p.SetActive(true);
                    p.GetComponent<ParticleSystem>().Play();
                    return;
                }
            }
            GameObject o = Instantiate(mainparticle, GameMng.Ins.skillMng.transform);
            o.transform.position = GameMng.Ins.player.transform.position;
            o.SetActive(true);
            o.GetComponent<ParticleSystem>().Play();
            mainParticleobj.Add(o);
        }
    }
    private void ReflectAttack()
    {
        var monsterpool = GameMng.Ins.monsterPool.monsterList;

        foreach(Monster m in monsterpool)
        {
            if (m == null || !m.active || !m.gameObject.activeSelf) continue;
            if((m.transform.position - transform.position).magnitude < radius)
            {
                m.Damage(Attacktype, GameMng.Ins.player.calStat.damage);
                GameMng.Ins.HitToEffect(Attacktype,
                    m.transform.position + new Vector3(0, m.monsterData.size),
                    gameObject.transform.position,
                    m.monsterData.size);
            }
        }
    }

    
}
