using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
	private float damage;
	private float width;
	private float height;
	private float DebufIndex;
	private float DebufActivePer;
	private float DebufEffectPer;
	private float DebufTime;

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
		delayTime = cooldownTime;
		dummy.Setting(3, 3, 500);
		gameObject.SetActive(false);
	}
	#endregion

	//TODO : SpriteDummy
	public Freezen dummy;

    public override void ActiveSkill()
    {
		base.ActiveSkill();
        dummy.gameObject.SetActive(true);
        dummy.transform.eulerAngles = GameMng.Ins.player.transform.eulerAngles + new Vector3(0, 0, 90);
        dummy.transform.position = GameMng.Ins.player.transform.position +
            GameMng.Ins.player.transform.right * height / 2;
    }

    void Update()
    {
		delayTime += Time.deltaTime;
		if (delayTime >= 0.5f)
			dummy.gameObject.SetActive(false);
    }
}
