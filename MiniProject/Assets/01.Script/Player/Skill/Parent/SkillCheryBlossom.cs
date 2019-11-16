using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheryBlossom : Skill
{
    #region SkillSetting
    enum sCheeryBlossom
    {
        Damage,
        CoolTime,
        FastSpeed,
        BufEndTime,
    }
    private float damage;
    private float fastSpeed;
    private float bufEndtime;
    public override void SkillSetting()
    {
        skillID = 15;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        damage = skillData.optionArr[(int)sCheeryBlossom.Damage];
        cooldownTime = skillData.optionArr[(int)sCheeryBlossom.CoolTime];
        fastSpeed = skillData.optionArr[(int)sCheeryBlossom.FastSpeed];
        bufEndtime = skillData.optionArr[(int)sCheeryBlossom.BufEndTime];
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
                fastSpeed += fastSpeed * changeValue;
                break;
            case eSkillOption.ActiveTime:
                bufEndtime += bufEndtime * changeValue;
                break;
        }
    }
    public override void SetBullet()
    {
        //foreach (SupporterIsLightning o in supporter)
        //    o.Setting(skillID, damage, attackspeed, skillendtime);
    }
    public override void OffSkill()
    {
        //foreach (SupporterIsLightning o in supporter)
        //    o.gameObject.SetActive(false);
    }
    #endregion

    //public List<SupporterIsLightning> supporter = new List<SupporterIsLightning>();

    //실제 쿨타임 도는 타이밍에 ActiveSkill();
    public override void OnButtonDown()
    {
        //GameMng.Ins.SetSkillAim(skillID);
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
    }
    public override void OnDrop(Vector2 pos)
    {
        base.OnDrop();
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
    }
}
