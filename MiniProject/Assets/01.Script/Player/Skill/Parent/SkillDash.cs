﻿using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDash : Skill
{
	#region SkillSetting
	enum eDashOption
	{
		Damage,
		coolTime,
	}

    eAttackType attackType = eAttackType.Wind;
    private float damage;
	List<Monster> Attack = new List<Monster>();
	public override void SkillSetting()
	{
		skillID = 1;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eDashOption.Damage];
		cooldownTime = skillData.optionArr[(int)eDashOption.coolTime];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
    }
	#endregion

	public Alter Bullet;

    private const int Count = 10;
    public List<Alter> alterList = new List<Alter>();
    

    bool AttackSet = false;


	public override void ActiveSkill()
	{
		base.ActiveSkill();
		Attack.Clear();
		//테스트 코드
		GameMng.Ins.player.playerStateMachine.ChangeState(ePlayerState.Dash);
		Vector3 direction = new Vector3(Mathf.Cos(GameMng.Ins.player.degree * Mathf.Deg2Rad),Mathf.Sin(GameMng.Ins.player.degree * Mathf.Deg2Rad), 0);
		for (int i = 0; i < Count; ++i)
		{
			alterList[i].Setting(GameMng.Ins.player.transform.position + new Vector3(0.01f, 0.25f, 0), direction, i+5);
		}
		Ray2D ray = new Ray2D(GameMng.Ins.player.transform.position, direction);
		//TODO : Range
		RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction,2);
		for(int i = 0; i < hits.Length; ++i)
		{
			if (hits[i].collider.CompareTag("Monster"))
			{
				Attack.Add(hits[i].collider.GetComponent<Monster>());
			}
		}
        AttackSet = true;
    }

    private void FinishAttack()
    {
		for(int i = 0; i < Attack.Count; ++i)
		{
			Debug.Log("대쉬어택");
			Attack[i].Damage(attackType, damage);
		}
    }

	private void Update()
	{
		delayTime += Time.deltaTime;
        int counting = 0;
        for(int i = 0;i<Count;++i)
        {
            if (!alterList[i].gameObject.activeSelf)
                ++counting;
        }
        if(counting == Count - 1 && AttackSet)
        {
            FinishAttack();
            AttackSet = false;
        }
	}
}
