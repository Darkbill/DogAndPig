using UnityEngine;
using GlobalDefine;

public class SkillIceNova : Skill
{
	#region SkillSetting
	enum eNovaSkillOption
	{
		Damage,
		CoolTime,
		DebufPer,
		SlowPer,
		DebufEndTime,
	}
	private float damage;
	private float debufPer;
	private float slowPer;
	private float debufEndTime;
	public override void SkillSetting()
	{
		skillID = 8;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eNovaSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eNovaSkillOption.CoolTime];
		delayTime = cooldownTime;
		debufPer = skillData.optionArr[(int)eNovaSkillOption.DebufPer];
		slowPer = skillData.optionArr[(int)eNovaSkillOption.SlowPer];
		debufEndTime = skillData.optionArr[(int)eNovaSkillOption.DebufEndTime];
		gameObject.SetActive(false);
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
		}
	}
	public override void SetBullet()
	{
		nova.Setting(skillID, debufPer /* slow per*/, damage, slowPer, debufEndTime);
		nova.transform.parent = GameMng.Ins.skillMng.transform;
	}
	public override void OffSkill()
	{
        nova.orbParticle.SetActive(false);
		nova.gameObject.SetActive(false);
	}
	#endregion

	public Nova nova;
	//실제 쿨타임 도는 타이밍에 ActiveSkill();
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		ActiveSkill();
        nova.SystemSetting();
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
