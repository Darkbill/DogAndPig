using UnityEngine;
using GlobalDefine;
using System.Collections.Generic;

public class SkillLinkIceBalt : Skill
{
	#region SkillSetting
	enum eIceBaltSkillOption
	{
		Damage,
		CoolTime,
		ActivePer,
		EndTime,
		ChangeValue,
		IceSpeed,
		MaxHitCount,
	}
	private float damage;
	private float buffActivePer;
	private float buffEndTime;
	private float buffChangeValue;
	private float iceSpeed;
	private int maxHitCount;
	public override void SkillSetting()
	{
		skillID = 10;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eIceBaltSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eIceBaltSkillOption.CoolTime];
		delayTime = cooldownTime;
		buffActivePer = skillData.optionArr[(int)eIceBaltSkillOption.ActivePer];
		buffEndTime = skillData.optionArr[(int)eIceBaltSkillOption.EndTime];
		buffChangeValue = skillData.optionArr[(int)eIceBaltSkillOption.ChangeValue];
		iceSpeed = skillData.optionArr[(int)eIceBaltSkillOption.IceSpeed];
		maxHitCount = (int)skillData.optionArr[(int)eIceBaltSkillOption.MaxHitCount];
		gameObject.SetActive(false);
		ice[0].transform.parent = GameMng.Ins.skillMng.transform;
	}
	public override void SetItemBuff(eSkillOption optionType, float changeValue)
	{
		switch (optionType)
		{
			case eSkillOption.Damage:
				damage += damage * changeValue;
				break;
			case eSkillOption.CoolTime:
				cooldownTime -= cooldownTime * changeValue;
				break;
			case eSkillOption.BuffActivePer:
				buffActivePer += buffActivePer * changeValue;
				break;
			case eSkillOption.BuffEndTime:
				buffEndTime += buffEndTime * changeValue;
				break;
			case eSkillOption.BuffChangeValue:
				changeValue += changeValue * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
        foreach (LinkIce o in ice)
            o.Setting(skillID, buffActivePer, damage, buffEndTime, buffChangeValue,
            iceSpeed,
            maxHitCount);
	}
	public override void OffSkill()
	{
        foreach (LinkIce o in ice)
            o.gameObject.SetActive(false);
	}
	#endregion
	public List<LinkIce> ice = new List<LinkIce>();

	//실제 쿨타임 도는 타이밍에 ActiveSkill();
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		ActiveSkill();
		BaltActive();
	}
	private void BaltActive()
	{
		for (int i = 0; i < ice.Count; ++i)
		{
			if (ice[i].gameObject.activeSelf) continue;
			ice[i].gameObject.SetActive(true);
			ice[i].SystemSetting();//TODO : 해당 세팅은 고정값 세팅이 아님 ㅎㅎ;
			base.ActiveSkill();
			return;
		}
		LinkIce o = Instantiate(ice[0], GameMng.Ins.skillMng.transform);
		o.Setting(skillID, buffActivePer, damage, buffEndTime, buffChangeValue,
		   iceSpeed,
		   maxHitCount);
		o.SystemSetting();
		ice.Add(o);
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}
}
