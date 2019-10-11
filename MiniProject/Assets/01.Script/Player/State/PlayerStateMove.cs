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
	float Range = 2.0f;
	float fSpeed = 13;

	Vector3 target = new Vector3();
	Vector3 movVec = new Vector3();

	public PlayerStateMove(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
		movVec = playerObject.transform.right * Range;
		target = playerObject.transform.position + movVec;
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
		if (att.BaseAttack(playerObject.transform.right,
					   GameMng.Ins.monster.transform.position - playerObject.transform.position,
					   playerObject.calStat.attackAngle,
					   playerObject.calStat.attackRange))
		{
			Debug.Log("플레이어 공격");
		}
	}

	private void Moving()
	{

		if (isDash == false)
		{
			fHorizontal = Input.GetAxis("Horizontal");
			fVertical = Input.GetAxis("Vertical");

			Mov.iSpeed = playerObject.calStat.moveSpeed;
			/////////////
			Ray2D rayUp = new Ray2D(playerObject.gameObject.transform.position, new Vector2(0,1));
			Ray2D rayDown = new Ray2D(playerObject.gameObject.transform.position, new Vector2(0, -1));
			Ray2D rayRight = new Ray2D(playerObject.gameObject.transform.position, new Vector2(1, 0));
			Ray2D rayLeft = new Ray2D(playerObject.gameObject.transform.position, new Vector2(-1, 0));
			RaycastHit2D hitUp;
			hitUp = Physics2D.Raycast(rayUp.origin, rayUp.direction, 0.15f, 1 << LayerMask.NameToLayer("Wall"));
			RaycastHit2D hitDown = Physics2D.Raycast(rayDown.origin, rayDown.direction, 0.1f, 1 << LayerMask.NameToLayer("Wall"));
			RaycastHit2D hitRight = Physics2D.Raycast(rayRight.origin, rayRight.direction, 0.1f, 1 << LayerMask.NameToLayer("Wall"));
			RaycastHit2D hitLeft = Physics2D.Raycast(rayLeft.origin, rayLeft.direction, 0.1f, 1 << LayerMask.NameToLayer("Wall"));
			if(hitUp.collider != null)
			{
				if (hitUp.collider.CompareTag("Wall"))
				{
					if(fVertical > 0) fVertical = 0;
				}
			}
			if (hitDown.collider != null)
			{
				if (hitDown.collider.CompareTag("Wall"))
				{
					fVertical = 0;
				}
			}
			if (hitRight.collider != null)
			{
				if (hitRight.collider.CompareTag("Wall"))
				{
					fHorizontal = 0;
				}
			}
			if (hitLeft.collider != null)
			{
				if (hitLeft.collider.CompareTag("Wall"))
				{
					fHorizontal = 0;
				}
			}
			/////////////
			playerObject.transform.position += Mov.Move(fHorizontal, fVertical);

			float Degree = Mathf.Atan2(fVertical, fHorizontal) * Mathf.Rad2Deg;
			playerObject.transform.eulerAngles = new Vector3(0, 0, Degree);
		}
		else
		{
			if (Vector3.Distance(playerObject.transform.position, target) > 0.5f)
			{
				playerObject.transform.position += movVec * Time.deltaTime * fSpeed;
			}
			else
			{
				playerObject.playerStateMachine.ChangeState(ePlayerState.Idle);
				isDash = false;
			}
		}
	}
}
