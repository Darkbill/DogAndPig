using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezen : BulletPlayerSkill
{
    public float damage = 0;
    eBuffType buffType = eBuffType.MoveSlow;

    private int Id = 0;
    private float MaxTimer = 10.0f;
    private float slow = 0.0f;
	private float per;

	public void Setting(int id,float mT,float s,float p)
	{
		Id = id;
		MaxTimer = mT;
		slow = s;
		per = p;
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(eAttackType.Water, GameMng.Ins.player.calStat.damage, damage, new ConditionData(buffType, Id, MaxTimer, slow), per);
	}
}