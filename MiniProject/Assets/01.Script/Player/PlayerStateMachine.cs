using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class PlayerStateMachine : MonoBehaviour
{
	public Dictionary<ePlayerState, PlayerState> stateDict = new Dictionary<ePlayerState, PlayerState>();
	public PlayerState cState;
	private void Awake()
	{
        Setting();
	}

    private void Setting()
	{
		stateDict.Add(ePlayerState.Idle, new PlayerStateIdle(this));
		stateDict.Add(ePlayerState.Move, new PlayerStateMove(this));
		stateDict.Add(ePlayerState.Dash, new PlayerStateDash(this));
		stateDict.Add(ePlayerState.Stun, new PlayerStateStun(this));
		stateDict.Add(ePlayerState.Dead, new PlayerStateDead(this));
		cState = stateDict[ePlayerState.Idle];
		cState.OnStart();
	}
	public void ChangeState(ePlayerState stateType)
	{
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}
	private void Update()
	{
		cState.Tick();
	}
}
