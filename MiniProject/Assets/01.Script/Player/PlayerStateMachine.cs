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
	public void Setting()
	{
		stateDict.Add(ePlayerState.Idle, new PlayerStateIdle(gameObject.GetComponent<Player>()));
		stateDict.Add(ePlayerState.Move, new PlayerStateMove(gameObject.GetComponent<Player>()));
		stateDict.Add(ePlayerState.Dash, new PlayerStateDash(gameObject.GetComponent<Player>()));
		stateDict.Add(ePlayerState.Stun, new PlayerStateStun(gameObject.GetComponent<Player>()));
		stateDict.Add(ePlayerState.Dead, new PlayerStateDead(gameObject.GetComponent<Player>()));
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
