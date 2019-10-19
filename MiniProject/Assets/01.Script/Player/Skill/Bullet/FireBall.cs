using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
using System;

public class FireBall : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;

	public void Setting(Vector3 pos,Vector3 moveVec)
	{
		gameObject.transform.position = pos;
		BulletMovVec = moveVec;
		gameObject.SetActive(true);
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(attackType, damage);
		gameObject.SetActive(false);
	}
}
