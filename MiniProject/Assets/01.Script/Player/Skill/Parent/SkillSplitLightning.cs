﻿using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSplitLightning : Skill
{
	#region SkillSetting
	enum eFloorFreezeOption
	{
		Damage,
		Duration,
		SturnPer,
		SturnTime,
		CoolTime,
	}
	private float damage;
	private float duration;
	private float sturnper;
	private float sturntime;

	const int MaxCount = 4;
	const int Angle180 = 180;
	const int SplitCnt = 5;

	public List<Lightning> BulletLst = new List<Lightning>();
	public GameObject lightning;

	public override void SkillSetting()
	{
		skillID = 5;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eFloorFreezeOption.Damage];
		duration = skillData.optionArr[(int)eFloorFreezeOption.Duration];
		sturnper = skillData.optionArr[(int)eFloorFreezeOption.SturnPer];
		sturntime = skillData.optionArr[(int)eFloorFreezeOption.SturnPer];
		cooldownTime = skillData.optionArr[(int)eFloorFreezeOption.CoolTime];
		delayTime = cooldownTime;
	}
	#endregion

	//TODO : SpriteDummy
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		ActiveSkill();
	}
	public override void ActiveSkill()
	{	
		base.ActiveSkill();
		int randnum = Rand.Random() % 90;
		CreateAndPoolBullet(randnum);
	}

	private void CreateAndPoolBullet(int randnum)
	{
		int Count = 0;
		for (int i = 0; Count < MaxCount; ++i)
		{
			if (BulletLst.Count == i)
			{
				GameObject light = Instantiate(
					lightning,
					GameMng.Ins.player.transform.position,
					Quaternion.Euler(0, 0, Angle180 * 2 / 4 * Count),// + randnum),
					gameObject.transform);
				light.GetComponent<Lightning>().Setting(skillID, SplitCnt, sturnper, damage);
				BulletLst.Add(light.GetComponent<Lightning>());
				++Count;
			}
			if (!BulletLst[i].gameObject.activeSelf)
			{
				BulletLst[i].transform.position = GameMng.Ins.player.transform.position;
				BulletLst[i].transform.rotation = Quaternion.Euler(0, 0, Angle180 * 2 / 4 * Count);// + randnum);
				BulletLst[i].Setting(skillID, SplitCnt, sturnper, damage);
				BulletLst[i].gameObject.SetActive(true);
				++Count;
			}
		}
	}

	void Update()
	{
		delayTime += Time.deltaTime;
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			int randnum = Rand.Range(-5, 5) * 5;
			if (BulletLst[i].SplitCheck && BulletLst[i].SplitCnt > 0)
			{
				BulletLst[i].SplitCheck = false;
				CreateBullet(BulletLst[i].EndPos, i);
				continue;
			}
			if (BulletLst[i].gameObject.activeSelf)
			{
				BulletLst[i].transform.position +=
					BulletLst[i].transform.right *
					Time.deltaTime *
					BulletLst[i].Speed;
				StartCoroutine(corrset(i));
				//                BulletLst[i].transform.eulerAngles += new Vector3(0, 0, randnum);
			}
		}
	}
	private IEnumerator corrset(int num)
	{
		yield return new WaitForSeconds(1.0f);
		int randnum = Rand.Range(-5, 5) * 5;
		BulletLst[num].transform.eulerAngles += new Vector3(0, 0, randnum);
	}
	private void CreateBullet(Vector3 endPos, int index)
	{
		int randnum = Rand.Random() % 90;
		int Count = 0;

		for (int i = 0; Count < MaxCount; ++i)
		{
			if (BulletLst.Count == i)
			{
				GameObject light = Instantiate(
					lightning,
					endPos,
					Quaternion.Euler(0, 0, Angle180 * 2 / 4 * Count + randnum),
					gameObject.transform);
				light.GetComponent<Lightning>().Setting(skillID,
					BulletLst[index].SplitCnt - 1,
					sturnper,
					damage);
				BulletLst.Add(light.GetComponent<Lightning>());
				++Count;
			}
			if (!BulletLst[i].gameObject.activeSelf)
			{
				BulletLst[i].transform.position = endPos;
				BulletLst[i].transform.rotation = Quaternion.Euler(0, 0, Angle180 * 2 / 4 * Count + randnum);
				BulletLst[i].Setting(skillID, BulletLst[index].SplitCnt - 1, sturnper, damage);
				BulletLst[i].gameObject.SetActive(true);
				++Count;
			}
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < BulletLst.Count; ++i)
			BulletLst[i].gameObject.SetActive(false);
	}
}
