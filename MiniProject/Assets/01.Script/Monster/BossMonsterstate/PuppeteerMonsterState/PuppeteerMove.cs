using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppeteerMove : MonsterStateBase
{
    Vector2 directionToPlayer;
    float degreeToPlayer;
    Vector3 ownerDirection = new Vector3(1, 0, 0);

    Vector3 movePos;
    float randCnt = 0;

    float fullHealthPoint;

    public PuppeteerMove(PuppeteerMonster o) : base(o)
    {
        fullHealthPoint = o.monsterData.healthPoint;
    }
    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Run);
        RandPositionSet();
    }

    public override bool OnTransition()
    {
        return true;
    }

    public override void Tick()
    {
        MonsterMove();
        if (OnTransition() == true) return;
        //MonsterMove();
    }
    public override void OnEnd()
    {

    }

    private void MonsterMove()
    {
        ownerDirection = monsterObject.GetForward();
        directionToPlayer = movePos - monsterObject.gameObject.transform.position;
        float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
        float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
        degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

        if (degreeToPlayer > 180) { degreeToPlayer -= 360; }
        else if (degreeToPlayer < -180) { degreeToPlayer += 360; }

        if (Mathf.Abs(degreeToPlayer) > 1)
        {
            if (degreeToPlayer < 0)
                monsterObject.Angle +=
                    Time.deltaTime * monsterObject.monsterData.rotationSpeed;
            else
                monsterObject.Angle -=
                    Time.deltaTime * monsterObject.monsterData.rotationSpeed;
        }
        if (directionToPlayer.magnitude > monsterObject.monsterData.attackRange)
        {
            monsterObject.gameObject.transform.position +=
                ownerDirection *
                Time.deltaTime *
                monsterObject.monsterData.moveSpeed;
        }
        else
        {
            monsterObject.StateMachine.ChangeStateIdle();
        }
    }

    private void RandPositionSet()
    {
        int fX = (int)DefineClass.MapSizX - 10;
        int fY = (int)DefineClass.MapSizY - 5;
        if( monsterObject.monsterData.healthPoint / fullHealthPoint < 0.5f && randCnt > 3)
        {
            Vector3 convers = monsterObject.transform.position - GameMng.Ins.player.transform.position;
            movePos = convers + monsterObject.transform.position;
            randCnt = 0;
            return;
        }
        movePos = new Vector3((float)Rand.Range(-fX, fX) / 10.0f, (float)Rand.Range(-fY, fY) / 10.0f, 0);
        ++randCnt;
    }
}
