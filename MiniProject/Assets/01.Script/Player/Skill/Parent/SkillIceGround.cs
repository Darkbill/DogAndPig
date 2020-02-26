using UnityEngine;
using GlobalDefine;
using System.Collections.Generic;

public class SkillIceGround : Skill
{
	#region SkillSetting
	enum eFloorFreezeOption
	{
		Damage,
		CoolTime,
		Width,
		Height,
		DebufIndex,
		DebufActivePer,
		DebugEffectPer,
		DebufTime,
		ActiveTime,
	}

	private float damage;
	private float width;
	private float height;
	private float DebufIndex;
	private float DebufActivePer;
	private float DebufEffectPer;
	private float DebufTime;
	private float activeTime;
	public override void SkillSetting()
	{
		skillID = 3;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eFloorFreezeOption.Damage];
		DebufIndex = skillData.optionArr[(int)eFloorFreezeOption.DebufIndex];
		DebufActivePer = skillData.optionArr[(int)eFloorFreezeOption.DebufActivePer];
		DebufTime = skillData.optionArr[(int)eFloorFreezeOption.DebufTime];
		width = skillData.optionArr[(int)eFloorFreezeOption.Width];
		height = skillData.optionArr[(int)eFloorFreezeOption.Height];
		cooldownTime = skillData.optionArr[(int)eFloorFreezeOption.CoolTime];
		DebufEffectPer = skillData.optionArr[(int)eFloorFreezeOption.DebugEffectPer];
		activeTime = skillData.optionArr[(int)eFloorFreezeOption.ActiveTime];
		delayTime = cooldownTime;
		FreezenShot[0].transform.parent = GameMng.Ins.skillMng.transform;
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
				DebufActivePer += DebufActivePer * changeValue;
				break;
			case eSkillOption.BuffChangeValue:
				DebufEffectPer += DebufEffectPer * changeValue;
				break;
			case eSkillOption.BuffEndTime:
				DebufTime += DebufTime * changeValue;
				break;
			case eSkillOption.ActiveTime:
				activeTime += activeTime * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		for (int i = 0; i < FreezenShot.Count; ++i)
			FreezenShot[i].Setting(skillID, DebufTime, DebufEffectPer, DebufActivePer, damage);
	}
	public override void OffSkill()
	{
		for (int i = 0; i < FreezenShot.Count; ++i)
			FreezenShot[i].gameObject.SetActive(false);
	}
	#endregion
	public List<Freezen> FreezenShot = new List<Freezen>();
	public override void OnButtonDown()
	{
		GameMng.Ins.SetSkillAim(skillID);
	}
	public override void OnDrop()
	{
		float degree = GameMng.Ins.player.degree;
		for (int i = 0; i < FreezenShot.Count; ++i)
		{
			if (FreezenShot[i].gameObject.activeSelf)
				continue;
			base.OnDrop();
			ActiveSkill();
			FreezenShot[i].angleSet(degree - 90, GameMng.Ins.player.transform.position);
			return;
		}
		ActiveSkill();
		Freezen o = Instantiate(FreezenShot[0], GameMng.Ins.skillMng.transform);
		o.Setting(skillID, DebufTime, DebufEffectPer, DebufActivePer, damage);
		o.angleSet(degree - 90, GameMng.Ins.player.transform.position);
		FreezenShot.Add(o);
	}

	public override void OnDrop(Vector2 pos)
	{
		float degree = GameMng.Ins.player.degree;
		for (int i = 0; i < FreezenShot.Count; ++i)
		{
			if (FreezenShot[i].gameObject.activeSelf)
				continue;
			base.OnDrop();
			ActiveSkill();
			FreezenShot[i].angleSet(degree - 90, GameMng.Ins.player.transform.position);
			return;
		}
		ActiveSkill();
		Freezen o = Instantiate(FreezenShot[0], GameMng.Ins.skillMng.transform);
		o.Setting(skillID, DebufTime, DebufEffectPer, DebufActivePer, damage);
		o.angleSet(degree - 90, GameMng.Ins.player.transform.position);
		FreezenShot.Add(o);
	}

	void Update()
	{
		delayTime += Time.deltaTime;
	}
}