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
		BuffType,
		EndTime,
		ChangeValue,
		IceSpeed,
		MaxHitCount,
	}
	private float damage;
	private float buffActivePer;
	private float buffEndTime;
	private float buffChangeValue;
	private eBuffType buffType;
	private float iceSpeed;
	private int maxHitCount;
	public override void SkillSetting()
	{
		skillID = 10;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eIceBaltSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eIceBaltSkillOption.CoolTime];
		delayTime = cooldownTime;
		buffActivePer = skillData.optionArr[(int)eIceBaltSkillOption.ActivePer];
		buffEndTime = skillData.optionArr[(int)eIceBaltSkillOption.EndTime];
		buffChangeValue = skillData.optionArr[(int)eIceBaltSkillOption.ChangeValue];
		buffType = (eBuffType)skillData.optionArr[(int)eIceBaltSkillOption.BuffType];
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
            o.Setting(skillID, buffActivePer, damage, skillType,
            buffType, buffEndTime, buffChangeValue,
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
	}
	public override void ActiveSkill()
	{
		for (int i = 0; i < ice.Count; ++i)
		{
			if (ice[i].gameObject.activeSelf) continue;
			ice[i].gameObject.SetActive(true);
            ice[i].Setting();//TODO : 해당 세팅은 고정값 세팅이 아님 ㅎㅎ;
            base.ActiveSkill();
			return;
		}
		LinkIce o = Instantiate(ice[0], GameMng.Ins.skillMng.transform);
		o.gameObject.SetActive(true);
        o.Setting();
        ice.Add(o);
        SetBullet();
        

        base.ActiveSkill();
	}
	public override void OnDrag()
	{
		base.OnDrag();
	}
	public override void OnDrop()
	{
		base.OnDrop();
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}
}
