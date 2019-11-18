using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolymerMonster : Monster
{
    private const float attackRadian = 1.0f;

    public SkillMucusAttack skill01;

    public bool skillFlag = false;

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

    public void MonsterSkillAtt()
    {
        skill01.gameObject.SetActive(true);
        skill01.SkillStart();
    }

    public void EndAttack()
    {
        Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
        if (att.BaseAttack(GetForward(), directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
        {
            GameMng.Ins.HitToEffect(eAttackType.Physics,
                GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size, 0),
                transform.position + new Vector3(0, monsterData.size, 0),
                GameMng.Ins.player.calStat.size);
            GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
        }
    }
}
