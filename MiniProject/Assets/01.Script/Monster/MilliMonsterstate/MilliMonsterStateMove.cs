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

    //Vector3 ownerDirection = new Vector3(1, 0, 0);
    float attackAngle = 0.0f;

    public MilliMonsterStateMove(MilliMonster o) : base(o)
    {

    }

    public override void OnStart()
    {
    }

    public override bool OnTransition()
    {
        delaytime += Time.deltaTime;
        Debug.DrawRay(monsterObject.gameObject.transform.position, monsterObject.transform.right);
        if (Mathf.Abs(degreeToPlayer) <= attackAngle && directionToPlayer.magnitude <= monsterObject.monsterData.attackRange)
        {
            if (delaytime > monsterObject.monsterData.attackSpeed)
            {
                delaytime = 0.0f;
                monsterObject.Attack();
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
        //Vector3 ownerDirection = monsterObject.transform.right;
        directionToPlayer = GameMng.Ins.player.transform.position - monsterObject.gameObject.transform.position;
        float ownerDegree = Mathf.Atan2(monsterObject.right.y, monsterObject.right.x);
        float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
        degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

        if (degreeToPlayer > 180) degreeToPlayer -= 360;
        else if (degreeToPlayer < -180) degreeToPlayer += 360;

        if (degreeToPlayer < 0)
            monsterObject.Angle +=
                Time.deltaTime * monsterObject.monsterData.rotationSpeed;
        else
            monsterObject.Angle -=
                Time.deltaTime * monsterObject.monsterData.rotationSpeed;

        monsterObject.SetObject(ownerDegree * Mathf.Rad2Deg);
        Debug.Log(ownerDegree);
        monsterObject.right = new Vector3(Mathf.Sign(monsterObject.Angle), Mathf.Cos(monsterObject.Angle * Mathf.Deg2Rad), 0);
        Debug.Log(monsterObject.right);
        //ownerDirection = new Vector3(ownerDirection.x * Mathf.Cos(degreeToPlayer) - ownerDirection.y * Mathf.Sin(degreeToPlayer),
        //                               ownerDirection.x * Mathf.Sin(degreeToPlayer) + ownerDirection.y * Mathf.Cos(degreeToPlayer), 0);  

        if (directionToPlayer.magnitude > monsterObject.monsterData.attackRange)
        {
            monsterObject.gameObject.transform.position +=
                monsterObject.right *
                Time.deltaTime *
                monsterObject.monsterData.moveSpeed;
            //monsterObject.condition.BufDebufUpdate().Speed;
        }
    }

}