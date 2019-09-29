using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateChase : MonsterState
{
    private bool attackcheck = false;

    private const float speed = 0.3f;

    private const float bullettime = 2.0f;

    private float shotime = 0.0f;

	public MonsterStateChase(MonsterStateMachine o) : base(o)
	{

	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{


		return false;
		//if ()
		//{
		//	return true;
		//}
		//return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
        ChaseToPlayerToGun();
	}
	public override void OnEnd()
	{

	}
	public void ChaseToPlayer()
	{
        //TODO : 일반적으로 따라오는 몬스터
		Vector3 direction = GameMng.Ins.player.transform.position - owner.transform.position;
		//owner.gameObject.transform.LookAt(direction);
		owner.gameObject.transform.Translate(new Vector3(direction.x * Time.deltaTime, 
                                             direction.y * Time.deltaTime),
                                             0);
	}

    public void ChaseToPlayerToGun()
    {
        //TODO : 따라오면서 총쏘는거 만들려고함
        Vector3 direction = GameMng.Ins.player.transform.position - owner.transform.position;
        owner.gameObject.transform.Translate(new Vector3(direction.x * Time.deltaTime * speed,
                                             direction.y * Time.deltaTime * speed),
                                             0);
        shotime += Time.deltaTime;
        if(shotime > bullettime)
        {
            shotime = 0.0f;
        }
    }
}
