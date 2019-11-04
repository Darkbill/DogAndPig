using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnFireArrow : Skill
{

	#region SkillSetting
	enum eSpawnFireArrowOption
	{
		Damage,
		CoolTime,
		ActiveTime,
	}
	private float damage;
	private float activeTime;
	public override void SkillSetting()
	{
		skillID = 7;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eSpawnFireArrowOption.Damage];
		cooldownTime = skillData.optionArr[(int)eSpawnFireArrowOption.CoolTime];
		activeTime = skillData.optionArr[(int)eSpawnFireArrowOption.ActiveTime];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
	}
	#endregion
	//실제 쿨타임 도는 타이밍에 ActiveSkill();
	public override void OnButtonDown()
	{
		base.OnButtonDown();
	}
	public override void OnDrag()
	{
		base.OnDrag();
	}
	public override void OnDrop()
	{
		base.OnDrop();
		ActiveSkill();
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}
}

