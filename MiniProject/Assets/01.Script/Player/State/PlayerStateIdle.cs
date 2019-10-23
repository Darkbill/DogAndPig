using UnityEngine;
using GlobalDefine;
public class PlayerStateIdle : PlayerState
{
	public PlayerStateIdle(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
		playerObject.ChangeAnimation(ePlayerAnimation.Idle);
	}

	public override bool OnTransition()
	{
		//TODO : 빌드 제한
		if(playerObject.isMove == true)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Move);
			return true;
		}
		else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Move);
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;

		if (playerObject.playerStateMachine.AttackDelay())
		{
			GameMng.Ins.player.AttackStart();
		}
	}
	public override void OnEnd()
	{

	}
}
