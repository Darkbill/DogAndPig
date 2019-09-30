using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateAttack : MonsterState
{
    private BulletPattern bulletPattern = new BulletPattern(eBulletType.Monster);

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
        bulletPattern.SettingPos(GameMng.Ins.player.transform.position, owner.transform.position);
        bulletPattern.OneShot();
    }
}
