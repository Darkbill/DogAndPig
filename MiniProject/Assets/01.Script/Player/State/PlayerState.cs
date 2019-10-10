using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public PlayerStateMachine playerObject;
	public PlayerState(PlayerStateMachine o)
	{
		playerObject = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
