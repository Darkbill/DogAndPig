using UnityEngine;
using GlobalDefine;
using System.Collections.Generic;

public class SkillFloorFreeze : Skill
{
	#region SkillSetting
	enum eFloorFreezeOption
	{
		Damage,
		Width,
		Height,
		CoolTime,
		DebufIndex,
		DebufActivePer,
		DebugEffectPer,
		DebufTime,
		BuffType,
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
	private eBuffType buffType;
	public override void SkillSetting()
	{
		skillID = 3;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eFloorFreezeOption.Damage];
		DebufIndex = skillData.optionArr[(int)eFloorFreezeOption.DebufIndex];
		DebufActivePer = skillData.optionArr[(int)eFloorFreezeOption.DebufActivePer];
		DebufTime = skillData.optionArr[(int)eFloorFreezeOption.DebufTime];
		width = skillData.optionArr[(int)eFloorFreezeOption.Width];
		height = skillData.optionArr[(int)eFloorFreezeOption.Height];
		cooldownTime = skillData.optionArr[(int)eFloorFreezeOption.CoolTime];
		DebufEffectPer = skillData.optionArr[(int)eFloorFreezeOption.DebugEffectPer];
		buffType = (eBuffType)skillData.optionArr[(int)eFloorFreezeOption.BuffType];
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
			FreezenShot[i].Setting(skillID, DebufTime, DebufEffectPer, DebufActivePer, damage, skillType, buffType);
	}
	public override void OffSkill()
	{
		for (int i = 0; i < FreezenShot.Count; ++i)
			FreezenShot[i].gameObject.SetActive(false);
	}
	public void SetCreateBullet()
	{
		Freezen obj = Instantiate(FreezenShot[0], GameMng.Ins.skillMng.transform);
		obj.gameObject.SetActive(false);
		FreezenShot.Add(obj);
	}
	#endregion
	public List<Freezen> FreezenShot = new List<Freezen>();
	public override void OnButtonDown()
	{
		GameMng.Ins.SetSkillAim(skillID);
	}
	public override void OnDrag()
	{
		base.OnDrag();
	}
	public override void OnDrop()
	{
		float degree = GameMng.Ins.player.degree;
		Vector3 pos = new Vector3(Mathf.Cos(degree), Mathf.Sin(degree), 0);
		for (int i = 0; i < FreezenShot.Count; ++i)
		{
			if (FreezenShot[i].gameObject.activeSelf)
				continue;
			base.OnDrop();
			ActiveSkill();
			FreezenShot[i].angleSet(degree - 90);
			FreezenShot[i].gameObject.SetActive(true);
			FreezenShot[i].transform.position = GameMng.Ins.player.transform.position;
			FreezenShot[i].transform.eulerAngles = new Vector3(0, 0, degree - 90);
			return;
		}
		base.OnDrop();
		ActiveSkill();
		Freezen o = Instantiate(FreezenShot[0], GameMng.Ins.skillMng.transform);
		o.angleSet(degree - 90);
		o.Setting(skillID, DebufTime, DebufEffectPer, DebufActivePer, damage, skillType, buffType);
		o.gameObject.SetActive(true);
		o.transform.position = GameMng.Ins.player.transform.position;
		o.transform.eulerAngles = new Vector3(0, 0, degree - 90);
		FreezenShot.Add(o);
	}
	void Update()
	{
		delayTime += Time.deltaTime;
	}
}