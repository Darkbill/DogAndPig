using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMonsterStateMove : MonsterState
{
    Vector2 directionToPlayer;
    float degreeToPlayer;
    Vector3 ownerDirection = new Vector3(1, 0, 0);

    float movetime;

    public WizardMonsterStateMove(WizardMonster o) : base(o)
    {
    }

    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Run);
        movetime = 0.0f;
    }

    public override bool OnTransition()
    {
        movetime += Time.deltaTime;
        if (movetime >= 2.0f)
        {
            monsterObject.monsterStateMachine.ChangeStateAttack();
            return true;
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
        ownerDirection = monsterObject.GetForward();
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

        if (directionToPlayer.magnitude > monsterObject.monsterData.attackRange)
        {
            monsterObject.gameObject.transform.position +=
                ownerDirection *
                Time.deltaTime *
                monsterObject.monsterData.moveSpeed;
        }
    }
}
