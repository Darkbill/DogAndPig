using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        //TODO : 따라오면서 총쏘는거 만듬
        
        Vector3 direction = GameMng.Ins.player.transform.position - owner.transform.position;
        owner.gameObject.transform.Translate(new Vector3(direction.x * Time.deltaTime * speed,
                                             direction.y * Time.deltaTime * speed),
                                             0);
        shotime += Time.deltaTime;
        //TODO : 단발
        //if(shotime > bullettime)
        //{
        //    shotime = 0.0f;
        //    GameMng.Ins.bulletMonster.OnBullet(GameMng.Ins.player.transform.position, owner.transform.position);
        //}

        //TODO : 연사 (bullettime임의설정)
        //if (shotime > 0.1f/*bullettime*/)
        //{
        //    shotime = 0.0f;
        //    Vector3 target = GameMng.Ins.player.transform.position + new Vector3(
        //        Random.Range(-0.3f, 0.3f), 
        //        Random.Range(-0.3f, 0.3f), 
        //        0);
        //    GameMng.Ins.bulletMonster.OnBullet(target, owner.transform.position);
        //}

        //TODO : 원형탄
        //if (shotime > bullettime/*bullettime*/)
        //{
        //    int siz = 10;//한번 발사할 때 탄의 개수.
        //    shotime = 0.0f;
        //    float radianstep = Mathf.PI * 2 / siz;//원주율 각도
        //    float radian = 0;
        //    for (int i = 0;i<siz;++i, radian += radianstep)
        //    {
        //        Vector3 target = GameMng.Ins.player.transform.position + new Vector3(
        //        Mathf.Cos(radian) * siz,
        //        Mathf.Sin(radian) * siz,
        //        0);
        //        GameMng.Ins.bulletMonster.OnBullet(target, owner.transform.position);
        //    }
        //}

        //TODO : 샷건(폭발탄 만드려다가 일단 이거로 쇼부) 수정 더해야함 ㅇㅇ
        if (shotime > bullettime/*bullettime*/)
        {
            int siz = 7;//한번 발사할 때 탄의 개수.
            shotime = 0.0f;
            float radianstep = Mathf.PI /180 * 15;//원주율 각도, 30은 theta값(발사각)
            float radian = Convert.ToBoolean(siz % 2) ? -siz / 2 * radianstep : siz / 2 * radianstep;
            for (int i = 0; i < siz; ++i, radian += radianstep)
            {
                Vector3 target = GameMng.Ins.player.transform.position + new Vector3(
                Mathf.Cos(radian) - Mathf.Sin(radian),
                Mathf.Sin(radian) + Mathf.Cos(radian),
                0);
                GameMng.Ins.bulletMonster.OnBullet(target, owner.transform.position);
            }
        }
    }
}
