using UnityEngine;
using GlobalDefine;
using System.Collections.Generic;

public class SkillRoundTrip : Skill
{
	#region SkillSetting
	enum eTripSkillOption
	{
		Damage,
		CoolTime,
		Range,
		tripSpeed,
	}
	private float damage;
	private float range;
	private float tripSpeed;
	public override void SkillSetting()
	{
		skillID = 11;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eTripSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eTripSkillOption.CoolTime];
		range = skillData.optionArr[(int)eTripSkillOption.Range];
		tripSpeed = skillData.optionArr[(int)eTripSkillOption.tripSpeed];
		delayTime = cooldownTime;
		gameObject.transform.parent = GameMng.Ins.skillMng.transform;
		gameObject.SetActive(false);
		recognition[0].transform.parent = GameMng.Ins.skillMng.transform;
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
			case eSkillOption.ActiveTime:
				range += range * changeValue;
				break;
			case eSkillOption.Speed:
				tripSpeed += tripSpeed * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		foreach(Recognition i in recognition)
		{
			i.Setting(damage, tripSpeed, range);
		}
	}
	public override void OffSkill()
	{
		for (int i = 0; i < recognition.Count; ++i)
		{
			recognition[i].gameObject.SetActive(false);
		}
	}
	#endregion

	public List<Recognition> recognition = new List<Recognition>();
	//실제 쿨타임 도는 타이밍에 ActiveSkill();
	public override void OnButtonDown()
	{
		GameMng.Ins.SetSkillAim(skillID);
	}
	public override void OnDrop()
	{
		base.OnDrop();
		ActiveSkill();
		Vector3 movevec = GameMng.Ins.player.GetForward();
		for (int i = 0; i < recognition.Count; ++i)
		{
			if (recognition[i].gameObject.activeSelf) continue;
			recognition[i].SystemSetting(GameMng.Ins.player.transform.position, movevec);
			return;
		}

		Recognition o = Instantiate(recognition[0], GameMng.Ins.skillMng.transform);
		o.Setting(damage, tripSpeed, range);
		o.SystemSetting(GameMng.Ins.player.transform.position, movevec);
		recognition.Add(o);
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}
}
