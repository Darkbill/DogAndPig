using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class PlayerStateMove : PlayerState
{
	public float fHorizontal { get; set; }
	public float fVertical { get; set; }
	public bool isMove;
	public Movement Mov = new Movement();

	public PlayerStateMove(Player o) : base(o)
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
			playerObject.playerStateMachine.ChangeState(ePlayerState.Idle);
		return true;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{

	}
    private void Attack()
	{
		ObjectSetAttack att = new ObjectSetAttack();
		if (att.BaseAttack(new Vector3(fHorizontal, fVertical, 0),
					   GameMng.Ins.monster.transform.position - playerObject.transform.position,
					   playerObject.calStat.attackAngle,
					   playerObject.calStat.attackRange))
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

		Mov.iSpeed = playerObject.calStat.moveSpeed;
		playerObject.transform.position += Mov.Move(fHorizontal, fVertical);

        float Degree = Mathf.Atan2(fVertical, fHorizontal) * Mathf.Rad2Deg;
        playerObject.transform.eulerAngles = new Vector3(0, 0, Degree);
	}

}
