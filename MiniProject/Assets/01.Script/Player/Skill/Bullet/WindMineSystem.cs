using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMineSystem : BulletPlayerSkill
{
    eAttackType attackType = eAttackType.Wind;
    eBuffType bufType = eBuffType.Stun;

    private float damage;
    private float bombDamage;
    private float radius;

    private const float burningTime = 2.0f;
    private float setTime = 0.0f;

    public CircleCollider2D minecollider2d;
    public GameObject minePreparations;
    public List<GameObject> minePreparationsParticleList = new List<GameObject>();
    public GameObject windMineBomb;
    public List<GameObject> windMineBombParticleList = new List<GameObject>();

    public void Setting(float _damage, float _bombDamage, float _radius)
    {
        damage = _damage;
        radius = _radius;
        bombDamage = _bombDamage;
        gameObject.transform.parent = GameMng.Ins.skillMng.transform;
        minePreparations.SetActive(false);
        windMineBomb.SetActive(false);

        foreach (GameObject p in minePreparationsParticleList)
            p.gameObject.transform.localScale *= _radius;

        foreach (GameObject p in windMineBombParticleList)
            p.gameObject.transform.localScale *= _radius;
    }

    public void SystemSetting(Vector3 _pos)
    {
        transform.position = _pos;
        minePreparations.transform.position = _pos;
        windMineBomb.transform.position = _pos + new Vector3(0, 0, -10f);

        setTime = 0.0f;

        minecollider2d.enabled = true;
        gameObject.SetActive(true);
    }

    public void EndSystemSetting()
    {
        minePreparations.SetActive(false);
        windMineBomb.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!minePreparations.GetComponent<ParticleSystem>().isPlaying && minePreparations.activeSelf)
        {
            EndSystemSetting();
        }
        if (minePreparations.activeSelf)
        {
            setTime += Time.deltaTime;
            if (setTime > burningTime)
                MineBomb();
        }
    }

    private void MineBomb()
    {
        windMineBomb.SetActive(true);

        var monsterpool = GameMng.Ins.monsterPool.monsterList;

        foreach(Monster m in monsterpool)
        {
            if (m == null || !m.gameObject.activeSelf) continue;
            if((m.transform.position - transform.position).magnitude < radius)
            {
                m.Damage(attackType, GameMng.Ins.player.calStat.damage, damage);
                GameMng.Ins.HitToEffect(attackType,
                    m.transform.position + new Vector3(0, m.monsterData.size),
                    gameObject.transform.position + new Vector3(-0.3f, -0.15f),
                    m.monsterData.size);
            }
        }
        setTime = -1f;
    }

    public override void Crash(Monster monster)
    {
        minecollider2d.enabled = false;
        minePreparations.SetActive(true);
        minePreparations.GetComponent<ParticleSystem>().Play();

        monster.Damage(attackType, GameMng.Ins.player.calStat.damage, damage);
        GameMng.Ins.HitToEffect(attackType,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position + new Vector3(-0.3f, -0.15f),
            monster.monsterData.size);
    }

}
