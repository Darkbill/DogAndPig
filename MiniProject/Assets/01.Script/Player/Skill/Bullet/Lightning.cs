﻿using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Lightning;
    eBuffType bufftype = eBuffType.Stun;

    private int Id = 0;
    private float MaxTimer = 0.0f;
    public float SetTimer = 0.0f;
    private float per;

    public Vector3 MoveVec;
    public Vector3 EndPos;
    public bool SplitCheck = false;
    public int SplitCnt;
    public float Speed;

    List<Lightning> lightningList = new List<Lightning>();

    public void Setting(int id, int splitcnt, float p, float damage)
    {
        Id = id;
        per = p;

        SetTimer = 0.0f;
        SplitCnt = splitcnt;
        MaxTimer = SplitCnt;
        Speed = Rand.Random() % 10 / 3 + 1f;
    }

    private void Update()
    {
        SetTimer += Time.deltaTime;
        if (SetTimer >= MaxTimer)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && SetTimer > 0.3f)
        {
            collision.GetComponent<Monster>().Damage(Attacktype, damage);
            collision.GetComponent<Monster>().OutStateAdd(new ConditionData(bufftype, Id, 10.0f, 500), 1000);
            EndPos = collision.transform.position;
            SplitCheck = true;
            gameObject.SetActive(false);
        }
    }
	public override void Crash(Monster monster)
	{
		monster.Damage(Attacktype, damage);
		monster.OutStateAdd(new ConditionData(bufftype, Id, 10.0f, 500), 1000);
		EndPos = monster.transform.position;
		SplitCheck = true;
		gameObject.SetActive(false);
	}
}
