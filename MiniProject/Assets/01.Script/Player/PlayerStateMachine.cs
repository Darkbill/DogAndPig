using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class PlayerStateMachine : MonoBehaviour
{
	public Dictionary<ePlayerState, PlayerState> stateDict = new Dictionary<ePlayerState, PlayerState>();
	public PlayerState cState;
	public bool isAttack = false;
	public float attackDelayTime;
	private void Awake()
	{
		Setting();
	}
	public void Setting()
	{
		Player o = gameObject.GetComponent<Player>();
		stateDict.Add(ePlayerState.Idle, new PlayerStateIdle(o));
		stateDict.Add(ePlayerState.Move, new PlayerStateMove(o));
		stateDict.Add(ePlayerState.Dash, new PlayerStateDash(o));
		stateDict.Add(ePlayerState.Stun, new PlayerStateStun(o));
		stateDict.Add(ePlayerState.Dead, new PlayerStateDead(o));
		cState = stateDict[ePlayerState.Idle];
		cState.OnStart();
	}
	public void ChangeState(ePlayerState stateType)
	{
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}
	public void ChangeStateIdle()
	{
		ChangeState(ePlayerState.Idle);
		cState.playerObject.ChangeAnimation(ePlayerAnimation.Idle);
		isAttack = false;
	}
	private void FixedUpdate()
	{
		attackDelayTime += Time.deltaTime;
		cState.Tick();
	}
	public bool AttackDelay()
	{
		if (isAttack == true) return false;
		else if (attackDelayTime >= GameMng.Ins.player.calStat.attackSpeed)
		{
			return true;
		}
		return false;
	}

}
