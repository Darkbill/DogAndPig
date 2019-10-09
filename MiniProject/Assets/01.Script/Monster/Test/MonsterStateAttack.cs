using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateAttack : MonsterState
{

	public MonsterStateAttack(MonsterStateMachine o) : base(o)
	{

	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
        Shotting();
        owner.ChangeState(eMonsterState.Chase);

        return true;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{

	}

    public void Shotting()
    {
        //TODO : 데코레이션패턴 테스트
        BulletPattern monsterpat = new BulletMonster();
        monsterpat.SettingPos(GameMng.Ins.player.transform.position, owner.transform.position, eBulletType.Monster);
        CircleShot Att01 = new CircleShot(monsterpat, 10);
        Att01.BulletShot();

        //bulletPattern.SettingPos(GameMng.Ins.player.transform.position, owner.transform.position);
        //bulletPattern.OneShot();
    }
}
