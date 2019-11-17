using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallHit : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Water;
    eBuffType bufftype = eBuffType.MoveSlow;
    private int id;
    private float per;
    private Vector3 moveset = Vector3.right;

    public bool AttackCheck = true;

    public ParticleSystem system;

    public void Setting(int _id, Vector3 _pos, float _damage)
    {
        id = _id;
        damage = _damage;
        gameObject.transform.position = _pos;
        system.Play();
    }

    private void Update()
    {
        if (!system.isPlaying)
        {
            gameObject.SetActive(false);
            AttackCheck = false;
        }
    }

    public override void Crash(Monster monster)
    {
        monster.Damage(Attacktype, GameMng.Ins.player.calStat.damage);
        GameMng.Ins.HitToEffect(Attacktype,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position,
            monster.monsterData.size);
    }
}
