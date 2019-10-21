using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class BossMonster : Monster
{
    public SkillBurningMeteor BossSkill01;
    public override void Attack()
    {
        base.Attack();
        StartCoroutine(AttackAnimationDelay());
    }
    IEnumerator AttackAnimationDelay()
    {
        Debug.Log("몬스터 공격");
        yield return new WaitForSeconds(1);
        monsterStateMachine.ChangeStateMove();
    }
    public override void Dead()
    {
        base.Dead();
        monsterStateMachine.ChangeStateDead();
    }
}
