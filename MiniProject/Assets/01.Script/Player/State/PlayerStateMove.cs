using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerState
{
    public float fHorizontal { get; set; }
    public float fVertical { get; set; }
    public bool isMove;
    public Movement Mov = new Movement();

    //TODO : Att 범위랑 반지름 길이
    const float AttDegree = 15;
    const float AttRange = 1.5f;

    const int Angle90 = 90;

    public PlayerStateMove(PlayerStateMachine o) : base(o)
	{
	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
        Moving();
        Attack();
        if (fVertical == 0 && fHorizontal == 0)
            playerObject.ChangeState(ePlayerState.Idle);
		return true;
	}

    private void Attack()
    {
        ObjectSetAttack att = new ObjectSetAttack();
        if (att.BaseAttack(new Vector3(fHorizontal, fVertical, 0),
                       GameMng.Ins.monster.transform.position - playerObject.transform.position,
                       AttRange,
                       AttDegree))
        {
            Debug.Log("플레이어 공격");
            //if (Rand.Percent(playerData.criticalChange))
            //    Debug.Log("Critical Hit");
            //GameMng.Ins.monster.Hit();
        }
    }

    private void Moving()
    {
        fHorizontal = Input.GetAxis("Horizontal");
        fVertical = Input.GetAxis("Vertical");

        Mov.iSpeed = playerData.moveSpeed;
        playerObject.transform.position += Mov.Move(fHorizontal, fVertical);

        //TODO : 플레이어의 방향이랑 공격유무 시각적으로 표현하기위해 추가한 코드.
        //Vector3 vMovement = new Vector3(
        //    0,
        //    0,
        //    (Mathf.Atan2(fHorizontal, fVertical)) * Mathf.Rad2Deg * -1 - Angle90);
    }

    public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{

	}
}
