using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDash : PlayerState
{
    float Range = 2.0f;
    float fSpeed = 13;

    Vector3 target = new Vector3();
    Vector3 movVec = new Vector3();

    public PlayerStateDash(Player o) : base(o)
    {
    }

    public override void OnStart()
    {
        movVec = playerObject.transform.right * Range;
        target = playerObject.transform.position + movVec;
    }

    public override bool OnTransition()
    {
        return true;
    }

    public override void Tick()
    {
        Dash();
        if (OnTransition() == true) return;

    }
    public override void OnEnd()
    {

    }
    public void Dash()
    {
        if (Vector3.Distance(playerObject.transform.position, target) > 0.5f)
        {
            playerObject.transform.position += movVec * Time.deltaTime * fSpeed;
        }
        else
        {
            playerObject.playerStateMachine.ChangeState(ePlayerState.Idle);
        }
    }
}
