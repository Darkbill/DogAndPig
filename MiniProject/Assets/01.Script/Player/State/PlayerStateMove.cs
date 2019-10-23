using UnityEngine;
using GlobalDefine;
public class PlayerStateMove : PlayerState
{
	public float fHorizontal;
	public float fVertical;
	public Movement Mov = new Movement();

	public PlayerStateMove(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
		playerObject.ChangeAnimation(ePlayerAnimation.Run);
    }

	public override bool OnTransition()
	{
		fHorizontal = Input.GetAxis("Horizontal");
		fVertical = Input.GetAxis("Vertical");
		if (playerObject.isMove == false)
		{
			return true;
		}
		else if (fVertical == 0 && fHorizontal == 0)
		{
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Idle);
			return;
		}
		Moving();
		Attack();
	}
	public override void OnEnd()
	{

	}
    private void Attack()
	{
		if (playerObject.playerStateMachine.AttackDelay())
		{
			GameMng.Ins.player.AttackStart();
		}
	}
	

	private void Moving()
	{
		Mov.iSpeed = playerObject.calStat.moveSpeed;
		playerObject.transform.position += Mov.Move(fHorizontal, fVertical);
		playerObject.degree = Mathf.Atan2(fVertical, fHorizontal) * Mathf.Rad2Deg;
    }
}
