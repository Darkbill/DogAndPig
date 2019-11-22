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

    bool notDash = false;
    public PlayerStateDash(Player o) : base(o)
    {
    }

    public override void OnStart()
    {
		dir = playerObject.GetForward();
		movVec = dir * Range;
        target = playerObject.transform.position + movVec;
        playerObject.GetComponent<CircleCollider2D>().enabled = false;
        notDash = false;
    }

    public override bool OnTransition()
    {
        Ray2D ray = new Ray2D(playerObject.transform.position + new Vector3(0, 0.35f, 0), dir);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.5f, 1 << LayerMask.NameToLayer("Wall"));
        if (hit.collider == null) return false;
        if (hit.collider.CompareTag("Wall"))
        {
            return true;
        }
        //TODO : 대쉬 몬스터 충돌 시 멈춤
        //var monsterpool = GameMng.Ins.monsterPool.monsterList;
        //notDash = false;
        //foreach (Monster o in monsterpool)
        //{
        //    if((target - o.transform.position).magnitude < o.monsterData.size + playerObject.calStat.size)
        //    {
        //        notDash = true;
        //        break;
        //    }
        //}
        //RaycastHit2D hitmonster = 
        //    Physics2D.Raycast(ray.origin, ray.direction, playerObject.calStat.size, 1 << LayerMask.NameToLayer("Default"));
        //if (hitmonster.collider.CompareTag("Monster") && notDash)
        //    return false;
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
        playerObject.GetComponent<CircleCollider2D>().enabled = true;
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
