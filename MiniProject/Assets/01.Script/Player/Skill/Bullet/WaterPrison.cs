using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPrison : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Water;
    eBuffType bufftype = eBuffType.MoveSlow;
    private int id = 14;
    private float per = 1000;
    private float slowPer = 1;
    private float endTime = 2.5f;

    public ParticleSystem system;
    public CircleCollider2D outcollider;
    public CircleCollider2D insidecollider;

    public void Setting(Vector3 _pos)
    {
        gameObject.transform.position = _pos;
        system.Play();
    }

    private void Update()
    {
        if (!system.isPlaying)
            gameObject.SetActive(false);
    }

    public override void Crash(Monster monster)
    {
        //if(monster == null)
        monster.Damage(Attacktype, 
            GameMng.Ins.player.calStat.damage, 
            damage, 
            new ConditionData(bufftype, id, endTime, slowPer), per);
    }
}
