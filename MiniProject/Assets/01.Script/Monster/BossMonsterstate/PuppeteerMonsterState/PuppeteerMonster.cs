using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppeteerMonster : Monster
{
    private const float jumpAttRadius = 1.7f;

    public ParticleSystem jumpOffEffect;
    public SkillTripleAttack skill01;

    public override void Dead()
    {
        StateMachine.ChangeStateDead();
        active = false;
        ColliderOnOff(false);
        GameMng.Ins.objectPool.goodmng.RunningSelect(1, 10, gameObject.transform.position);
        GameMng.Ins.objectPool.goodmng.RunningSelect(2, 5, gameObject.transform.position);
        GameMng.Ins.AddExp(true);
    }
    public override void DamageResult(int d)
    {
        if (d < 1) d = 1;
        monsterData.healthPoint -= d;
        UIMngInGame.Ins.DamageToBoss(d, transform.position);
        if (monsterData.healthPoint <= 0) Dead();
    }

    public void AllClear()
    {
        GameMng.Ins.WorldClear();
    }

    //애니메이션 호출 함수
    public void MonstetSkillJumpOn()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    //애니메이션 호출 함수
    public void MonsterSkillJumpOff()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        CheckCircleSetting();
    }

    private void CheckCircleSetting()
    {
        float range = (GameMng.Ins.player.transform.position - gameObject.transform.position).magnitude;
        if(range < jumpAttRadius)
        {
            //PlayerHit
            GameMng.Ins.player.Damage(eAttackType.Wind, monsterData.damage * 2);
            GameMng.Ins.HitToEffect(eAttackType.Wind,
                GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size),
                gameObject.transform.position + new Vector3(0, monsterData.size),
                GameMng.Ins.player.calStat.size);
            GameMng.Ins.player.OutStateAdd(new ConditionData(eBuffType.NockBack, 1, 0, range * 5),
                GameMng.Ins.player.transform.position - transform.position);
        }
        jumpOffEffect.gameObject.transform.localScale = new Vector3(jumpAttRadius, jumpAttRadius, jumpAttRadius);
        jumpOffEffect.gameObject.transform.position = transform.position;
        jumpOffEffect.Play();
    }

    //애니메이션 호출 함수
    public void MonsterSkillAtt()
    {
        skill01.gameObject.SetActive(true);
        skill01.SkillStart(this);
    }


    public void EndAttack()
    {
        //Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
        //if (att.BaseAttack(GetForward(), directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
        //{
        //    GameMng.Ins.HitToEffect(eAttackType.Physics,
        //        GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size, 0),
        //        transform.position + new Vector3(0, monsterData.size, 0),
        //        GameMng.Ins.player.calStat.size);
        //    GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
        //}
    }
}
