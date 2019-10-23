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

#if UNITY_EDITOR_WIN
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Move);
			return true;
		}
#else
        if(playerObject.isMove == true)
        {
			playerObject.playerStateMachine.ChangeState(ePlayerState.Move);
			return true;
		}
        return false;
#endif
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
