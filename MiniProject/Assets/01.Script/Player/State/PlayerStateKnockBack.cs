using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateKnockBack : PlayerState
{
    private float setspeed;
    private Vector3 range;

    public PlayerStateKnockBack(Player o) : base(o)
    {
    }

    public override void OnStart()
    {
        setspeed = Define.knockBackSpeed * playerObject.playerStateMachine.knockBackPower;
        range = playerObject.playerStateMachine.knockBackDir;
        range.Normalize();
    }

    public override bool OnTransition()
    {
        Ray2D[] rayarray = new Ray2D[4];
        rayarray[0] = new Ray2D(playerObject.transform.position + new Vector3(0, playerObject.calStat.size),
            Vector3.right);
        rayarray[1] = new Ray2D(playerObject.transform.position + new Vector3(0, playerObject.calStat.size),
            -Vector3.right);
        rayarray[2] = new Ray2D(playerObject.transform.position + new Vector3(0, playerObject.calStat.size),
            Vector3.up);
        rayarray[3] = new Ray2D(playerObject.transform.position + new Vector3(0, playerObject.calStat.size),
            -Vector3.up);

        foreach (Ray2D ray in rayarray)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.5f, 1 << LayerMask.NameToLayer("Wall"));
            if (hit.collider == null) continue;
            if (hit.collider.CompareTag("Wall"))
                return true;
        }

        if (setspeed <= 0)
        {
            return true;
        }
        return false;
    }

    public override void Tick()
    {
        if (OnTransition() == true)
        {
            playerObject.playerStateMachine.ChangeStateIdle();
            return;
        }
        KnockBack();
    }

    private void KnockBack()
    {
        playerObject.transform.position += range * Time.deltaTime * setspeed;
        setspeed -= Time.deltaTime * 100;
    }
    public override void OnEnd()
    {

    }
}
