using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GlobalDefine;

public class MonsterStateChase : MonsterState
{

    private const float speed = 0.3f;

    private const float bullettime = 2.0f;

    private float delaytime = 0.0f;

	private float rotateSpeed = 100f;

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
		Vector3 ownerDirection = owner.gameObject.transform.right;
		Vector3 goalDirection = GameMng.Ins.player.transform.position - owner.gameObject.transform.position;
		float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
		float goalDegree = Mathf.Atan2(goalDirection.y, goalDirection.x);
		float degree = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

		if (degree > 180) degree -= 360;
		else if (degree < -180) degree += 360;

		if (degree < 0) owner.transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * rotateSpeed);
		else			owner.transform.eulerAngles -= new Vector3(0, 0, Time.deltaTime * rotateSpeed);

		owner.gameObject.transform.position += owner.gameObject.transform.right * Time.deltaTime * speed;
	}

}
