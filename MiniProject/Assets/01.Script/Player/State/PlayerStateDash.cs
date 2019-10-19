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
	Vector3 dir;
    public PlayerStateDash(Player o) : base(o)
    {
    }

    public override void OnStart()
    {
		dir = new Vector3(Mathf.Cos(GameMng.Ins.player.degree * Mathf.Deg2Rad), Mathf.Sin(GameMng.Ins.player.degree * Mathf.Deg2Rad), 0);
		movVec = dir * Range;
        target = playerObject.transform.position + movVec;
    }

    public override bool OnTransition()
    {
        Ray2D ray = new Ray2D(playerObject.transform.position, dir);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.5f, 1 << LayerMask.NameToLayer("Wall"));
        if (hit.collider == null) return false;
        if (hit.collider.CompareTag("Wall"))
        {
            return true;
        }
        return false;
    }

    public override void Tick()
    {

        if (OnTransition() == true)
        {
            playerObject.playerStateMachine.ChangeState(ePlayerState.Move);
            return;
        }
        Dash();
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
