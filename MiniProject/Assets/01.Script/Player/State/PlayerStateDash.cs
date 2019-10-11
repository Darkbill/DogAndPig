using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDash : PlayerState
{
    float Range = 2.0f;

    Vector3 target = new Vector3();

	public PlayerStateDash(Player o) : base(o)
	{
	}

	public override void OnStart()
	{

	}

	public override bool OnTransition()
	{
		return true;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
        Dash();
    }
	public override void OnEnd()
	{

	}
    public void Dash()
    {
        
    }
}
