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
	private void FixedUpdate()
	{
		cState.Tick();
	}
	public void AttackDelay()
	{
		StartCoroutine(IEAttackDelay());
	}
	private IEnumerator IEAttackDelay()
	{
		yield return new WaitForSeconds(cState.playerObject.calStat.attackSpeed);
	}
}
