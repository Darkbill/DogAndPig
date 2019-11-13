﻿using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillThounderCalling : Skill
{
	#region SkillSetting
	enum eThoumderOption
	{
		Damage,
		CoolTime,
		MaxCount,
		RandRange,
	}
	//TODO : randrange는 해당 값의 /10하고 나오는 값.
	private float damage;
	private float maxcount;
	private int randrange;
	public override void SkillSetting()
	{
		skillID = 13;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eThoumderOption.Damage];
		cooldownTime = skillData.optionArr[(int)eThoumderOption.CoolTime];
		maxcount = skillData.optionArr[(int)eThoumderOption.MaxCount];
		randrange = (int)skillData.optionArr[(int)eThoumderOption.RandRange];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
	}
	public override void SetItemBuff(eSkillOption type, float changeValue)
	{
		switch (type)
		{
			case eSkillOption.Damage:
				damage += damage * changeValue;
				break;
			case eSkillOption.CoolTime:
				cooldownTime -= cooldownTime * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
        foreach (Thounder o in thounderlist)
            o.Setting(damage);
	}
	public override void OffSkill()
	{
        foreach (Thounder o in thounderlist)
            o.gameObject.SetActive(false);
	}
	#endregion

	public List<Thounder> thounderlist = new List<Thounder>();
	private int count = 0;
	private Vector3 startpos = new Vector3();
	private bool skillOn = false;


	public override void OnButtonDown()
	{
		GameMng.Ins.SetSkillAim(skillID);
		ActiveSkill();
		count = 0;
	}
	public override void OnDrop()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		startpos = new Vector3(mousePos.x, mousePos.y, 0);
		skillOn = true;
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
		if (skillOn && count <= maxcount)
		{
			SkillCall();
		}
	}
	//TODO : 성민형 과제
	private void SkillCall()
	{
		thounderlist[0].transform.position = startpos;
		thounderlist[0].gameObject.SetActive(true);
		thounderlist[0].isPlay();

		for (int i = 1; i < thounderlist.Count; ++i)
		{
			float randx = (float)Rand.Range(-randrange, randrange) / 10.0f;
			float randy = (float)Rand.Range(-randrange, randrange) / 10.0f;
			if (count + 1 <= i)
				break;
			thounderlist[i].transform.position =
				thounderlist[i - 1].transform.position +
				new Vector3(randx, randy);
			thounderlist[i].gameObject.SetActive(true);
			thounderlist[i].isPlay();
		}
		++count;
		float x = (float)Rand.Range(-randrange, randrange) / 10.0f;
		float y = (float)Rand.Range(-randrange, randrange) / 10.0f;
		startpos = startpos + new Vector3(x, y);
		skillOn = false;
		StartCoroutine(SkillHitCheck());
	}
	private IEnumerator SkillHitCheck()
	{
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < thounderlist.Count; ++i)
		{
			if (thounderlist[i].hit)
			{
				thounderlist[i].hit = false;
				skillOn = true;
			}
		}
	}
}
