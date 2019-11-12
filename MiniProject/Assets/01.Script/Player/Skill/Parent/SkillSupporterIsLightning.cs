﻿using GlobalDefine;
using System.Collections;
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
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eSupporterIsLightningSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eSupporterIsLightningSkillOption.CoolTime];
        attackspeed = skillData.optionArr[(int)eSupporterIsLightningSkillOption.AttackSpeed];
        skillendtime = skillData.optionArr[(int)eSupporterIsLightningSkillOption.SkillEndTime];
        delayTime = cooldownTime;
        supporter[0].gameObject.transform.parent = GameMng.Ins.skillMng.transform;
        gameObject.SetActive(false);
    }

    public override void SetItemBuff(eSkillOption optionType, float changeValue)
    {
        switch (optionType)
        {
            case eSkillOption.Damage:
                damage += damage * changeValue;
                break;
                //TODO : skill15의 옵션은 소환수의 공격속도로?
            case eSkillOption.CoolTime:
                attackspeed += attackspeed * changeValue;
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
            SetBullet();
            supporter[i].SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            supporter[i].gameObject.SetActive(true);
            supporter[i].StartSupporterAttack();
            return;
        }

        SupporterIsLightning o = Instantiate(supporter[0], GameMng.Ins.skillMng.transform);
        supporter.Add(o);
        SetBullet();
        o.SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        o.gameObject.SetActive(true);
        o.StartSupporterAttack();
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
    }
}