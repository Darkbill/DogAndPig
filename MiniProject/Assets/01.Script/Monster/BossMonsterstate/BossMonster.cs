using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class BossMonster : Monster
{
    public BossMonsterStateMachine monsterStateMachine;
    public override void Attack()
    {
        base.Attack();
        StartCoroutine(AttackAnimationDelay());
    }
    IEnumerator AttackAnimationDelay()
    {
        Debug.Log("몬스터 공격");
        yield return new WaitForSeconds(1);
        monsterStateMachine.ChangeState(eBossMonsterState.Move);
    }
    public override void Dead()
    {
        base.Dead();
        monsterStateMachine.ChangeState(eBossMonsterState.Dead);
    }
}
