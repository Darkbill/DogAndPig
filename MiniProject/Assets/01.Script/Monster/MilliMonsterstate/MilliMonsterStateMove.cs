using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GlobalDefine;

public class MilliMonsterStateMove : MonsterState
{

    private float delaytime = 0.0f;
    Vector2 directionToPlayer;
    float degreeToPlayer;

    Vector3 ownerDirection = new Vector3(1, 0, 0);
    float attackAngle = 0.0f;

    public MilliMonsterStateMove(MilliMonster o) : base(o)
    {

    }

    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Run);
    }

    public override bool OnTransition()
    {
        delaytime += Time.deltaTime;
        Debug.DrawRay(monsterObject.gameObject.transform.position, ownerDirection);
        if (Mathf.Abs(degreeToPlayer) <= monsterObject.monsterData.attackAngle && 
            directionToPlayer.magnitude <= monsterObject.monsterData.attackRange)
        {
            if (delaytime > monsterObject.monsterData.attackSpeed)
            {
                delaytime = 0.0f;
                monsterObject.Attack();
                monsterObject.ChangeAnimation(eMonsterAnimation.Attack);
                monsterObject.monsterStateMachine.ChangeStateIdle();
                return true;
            }
        }
        return false;
    }

    public override void Tick()
    {
        if (OnTransition() == true) return;
        ChaseToPlayer();
    }
    public override void OnEnd()
    {

    }
    public void ChaseToPlayer()
    {
        directionToPlayer = GameMng.Ins.player.transform.position - monsterObject.gameObject.transform.position;
        float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
        float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
        degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

        if (degreeToPlayer > 180) { degreeToPlayer -= 360; }
        else if (degreeToPlayer < -180) { degreeToPlayer += 360; }

        if (degreeToPlayer < 0)
            monsterObject.Angle +=
                Time.deltaTime * monsterObject.monsterData.rotationSpeed;
        else
            monsterObject.Angle -=
                Time.deltaTime * monsterObject.monsterData.rotationSpeed;

        Vector3 ownerset = new Vector3(1, 0, 0);

        ownerDirection = new Vector3(ownerset.x * Mathf.Cos(monsterObject.Angle * Mathf.Deg2Rad) - ownerset.y * Mathf.Sin(monsterObject.Angle * Mathf.Deg2Rad),
                                       ownerset.x * Mathf.Sin(monsterObject.Angle * Mathf.Deg2Rad) + ownerset.y * Mathf.Cos(monsterObject.Angle * Mathf.Deg2Rad), 0);  

        if (directionToPlayer.magnitude > monsterObject.monsterData.attackRange)
        {
            monsterObject.gameObject.transform.position +=
                ownerDirection *
                Time.deltaTime *
                monsterObject.monsterData.moveSpeed;
            //monsterObject.condition.BufDebufUpdate().Speed;
        }
    }

}