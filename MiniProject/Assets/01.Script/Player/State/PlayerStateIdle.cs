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
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Move);
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;

		if (playerObject.playerStateMachine.attackDelayTime >= playerObject.calStat.attackSpeed)
		{
			GameMng.Ins.player.AttackCheck();
		}
	}
	public override void OnEnd()
	{

	}
}
