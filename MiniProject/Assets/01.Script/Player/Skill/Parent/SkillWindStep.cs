﻿using GlobalDefine;
using UnityEngine;

public class SkillWindStep : Skill
{
    #region SkillSetting
    enum eWindStepOption
    {
        BufTime,
        CoolTime,
        SpeedPer,
        Radius,
    }
    private float buftime;
    private float speedPer;
    private float radius;

    public override void SkillSetting()
    {
        skillID = 16;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        buftime = skillData.optionArr[(int)eWindStepOption.BufTime];
        cooldownTime = skillData.optionArr[(int)eWindStepOption.CoolTime];
        speedPer = skillData.optionArr[(int)eWindStepOption.SpeedPer];
        radius = skillData.optionArr[(int)eWindStepOption.Radius];
        delayTime = cooldownTime;
		gameObject.SetActive(false);
    }
    public override void SetItemBuff(eSkillOption type, float changeValue)
    {
        switch (type)
        {
            case eSkillOption.ActiveTime:
                buftime += buftime * changeValue;
                break;
            case eSkillOption.CoolTime:
                cooldownTime -= cooldownTime * changeValue;
                break;
            case eSkillOption.Speed:
                speedPer += speedPer * changeValue;
                break;
            case eSkillOption.BuffActivePer:
                radius += radius * changeValue;
                break;
        }
    }
    public override void SetBullet()
    {
        windbullet.Setting(skillID, buftime, radius, speedPer);
    }
    public override void OffSkill()
    {
        windbullet.OffSkill();
		if (GameMng.Ins.isRecord)
		{
			gameObject.SetActive(false);
		}
	}
    #endregion

    public WindKnockback windbullet;

    public override void OnButtonDown()
    {
        base.OnButtonDown();
        ActiveSkill();
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        windbullet.SystemSetting();
    }

    void Update()
    {
        delayTime += Time.deltaTime;
    }

}
