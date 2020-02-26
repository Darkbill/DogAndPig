using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;

public class SkillLightningShock : Skill
{
	#region SkillSetting
	enum eFloorFreezeOption
	{
		Damage,
		CoolTime,
		SturnPer,
		SturnTime,
		EndTime,
	}
	private float damage;
	private float sturnper;
	private float sturntime;
	const int MaxCount = 4;
	const int Angle180 = 180;
	const int SplitCnt = 5;
	private float buffEndTime;
	public List<Lightning> BulletLst = new List<Lightning>();
	public GameObject lightning;

	public override void SkillSetting()
	{
		skillID = 5;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eFloorFreezeOption.Damage];
		sturnper = skillData.optionArr[(int)eFloorFreezeOption.SturnPer];
		sturntime = skillData.optionArr[(int)eFloorFreezeOption.SturnPer];
		cooldownTime = skillData.optionArr[(int)eFloorFreezeOption.CoolTime];
		buffEndTime = skillData.optionArr[(int)eFloorFreezeOption.EndTime];
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
			case eSkillOption.BuffActivePer:
				sturnper += sturnper * changeValue;
				break;
			case eSkillOption.BuffEndTime:
				sturntime += sturntime * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		foreach (Lightning o in BulletLst)
		{
			o.Setting(skillID, sturnper, damage, buffEndTime);
		}
	}
	public override void OffSkill()
	{
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			BulletLst[i].OffSkillSet();
		}
	}
	#endregion

	public override void OnButtonDown()
	{
		base.OnButtonDown();
		ActiveSkill();
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
				GameObject light = Instantiate(lightning, GameMng.Ins.skillMng.transform);
				light.GetComponent<Lightning>().Setting(skillID, sturnper, damage, buffEndTime);
				light.GetComponent<Lightning>().SystemSetting(GameMng.Ins.player.transform.position,
					Quaternion.Euler(0, 0, Angle180 * 2 / 4 * Count),
					SplitCnt);
				BulletLst.Add(light.GetComponent<Lightning>());
				++Count;
			}
			if (!BulletLst[i].gameObject.activeSelf)
			{
				BulletLst[i].SystemSetting(GameMng.Ins.player.transform.position,
					Quaternion.Euler(0, 0, Angle180 * 2 / 4 * Count),
					SplitCnt);
				++Count;
			}
		}
	}
	void Update()
	{
		delayTime += Time.deltaTime;
	}
}
