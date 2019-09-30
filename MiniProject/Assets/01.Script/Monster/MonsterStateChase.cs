using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GlobalDefine;

public class MonsterStateChase : MonsterState
{
    private bool attackcheck = false;

    private const float speed = 0.3f;

    private const float bullettime = 2.0f;

    private float delaytime = 0.0f;

	public MonsterStateChase(MonsterStateMachine o) : base(o)
	{

	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
        delaytime += Time.deltaTime;
        if (delaytime > bullettime)
        {
            delaytime = 0.0f;
            owner.ChangeState(eMonsterState.Attack);
            return true;
        }
        return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
        ChaseToPlayer();
	}
	public override void OnEnd()
	{

	}
	public void ChaseToPlayer()
	{
        //TODO : 일반적으로 따라오는 몬스터
		Vector3 direction = GameMng.Ins.player.transform.position - owner.transform.position;
		//owner.gameObject.transform.LookAt(direction);//2d엔 조금 수정해야할듯
		owner.gameObject.transform.Translate(new Vector3(direction.x * Time.deltaTime * speed, 
                                             direction.y * Time.deltaTime * speed),
                                             0);
	}
}
