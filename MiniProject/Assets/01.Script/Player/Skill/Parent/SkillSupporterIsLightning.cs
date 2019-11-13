﻿using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;

public class SkillSupporterIsLightning : Skill
{
    #region SkillSetting
    enum eSupporterIsLightningSkillOption
    {
        Damage,
        CoolTime,
        AttackSpeed,
        SkillEndTime,
    }
    private float damage;
    private float attackspeed;
    private float skillendtime;
    public override void SkillSetting()
    {
        skillID = 15;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        damage = skillData.optionArr[(int)eSupporterIsLightningSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eSupporterIsLightningSkillOption.CoolTime];
        attackspeed = skillData.optionArr[(int)eSupporterIsLightningSkillOption.AttackSpeed];
        skillendtime = skillData.optionArr[(int)eSupporterIsLightningSkillOption.SkillEndTime];
        delayTime = cooldownTime;
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
			case eSkillOption.Speed:
				attackspeed += attackspeed * changeValue;
				break;
			case eSkillOption.ActiveTime:
				skillendtime += skillendtime * changeValue;
				break;
        }
    }
    public override void SetBullet()
    {
        foreach(SupporterIsLightning o in supporter)
            o.Setting(skillID, damage, attackspeed, skillendtime);
    }
    public override void OffSkill()
    {
        foreach (SupporterIsLightning o in supporter)
            o.gameObject.SetActive(false);
    }
    #endregion

    public List<SupporterIsLightning> supporter = new List<SupporterIsLightning>();

    //실제 쿨타임 도는 타이밍에 ActiveSkill();
    public override void OnButtonDown()
    {
        GameMng.Ins.SetSkillAim(skillID);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
    }
    public override void OnDrag()
    {
        base.OnDrag();
    }
    public override void OnDrop()
    {
        //TODO : Test
        base.OnDrop();
        for(int i = 0;i<supporter.Count;++i)
        {
            if (supporter[i].gameObject.activeSelf) continue;
            supporter[i].SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            return;
        }

        SupporterIsLightning o = Instantiate(supporter[0], GameMng.Ins.skillMng.transform);
        supporter.Add(o);
		o.Setting(skillID, damage, attackspeed, skillendtime);
		o.SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
    }
}
