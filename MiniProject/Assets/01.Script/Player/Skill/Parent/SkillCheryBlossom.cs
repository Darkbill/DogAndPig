using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheryBlossom : Skill
{
    #region SkillSetting
    enum sCheryBlossom
    {
        Damage,
        CoolTime,
        Radius,
        BufEndTime,
    }
    private float damage;
    private float radius;
    private float bufEndtime;
    public override void SkillSetting()
    {
        skillID = 17;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        damage = skillData.optionArr[(int)sCheryBlossom.Damage];
        cooldownTime = skillData.optionArr[(int)sCheryBlossom.CoolTime];
        radius = skillData.optionArr[(int)sCheryBlossom.Radius];
        bufEndtime = skillData.optionArr[(int)sCheryBlossom.BufEndTime];
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
            case eSkillOption.ActiveTime:
                bufEndtime += bufEndtime * changeValue;
                break;
        }
    }
    public override void SetBullet()
    {
        blossom.Setting(damage, delayTime, radius);
    }
    public override void OffSkill()
    {
        blossom.gameObject.SetActive(false);
    }
    #endregion

    public CheryBlossomSystem blossom;

    //실제 쿨타임 도는 타이밍에 ActiveSkill();
    public override void OnButtonDown()
    {
        blossom.SystemSetting();
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
