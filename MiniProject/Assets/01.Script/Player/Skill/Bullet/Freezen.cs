﻿using GlobalDefine;
using UnityEngine;

public class Freezen : BulletPlayerSkill
{
    public float damage = 0;
	eBuffType buffType;
	eAttackType attackType;
    private int Id = 0;
    private float MaxTimer = 10.0f;
    private float slow = 0.0f;
	private float per;

    private Vector3 rightvec;

	public void Setting(int id,float mT,float s,float p,float d,eAttackType aType,eBuffType bType)
	{
		Id = id;
		MaxTimer = mT;
		slow = s;
		per = p;
		damage = d;
		attackType = aType;
		buffType = bType;
        
	}

    public void angleSet(float angle)
    {
        rightvec = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    }

	public override void Crash(Monster monster)
	{
		monster.Damage(attackType, GameMng.Ins.player.calStat.damage, damage, new ConditionData(buffType, Id, MaxTimer, slow), per);
		GameMng.Ins.HitToEffect(attackType, 
            monster.transform.position + new Vector3(0, monster.monsterData.size), 
            GameMng.Ins.player.transform.position + rightvec,
            monster.monsterData.size);
	}
}