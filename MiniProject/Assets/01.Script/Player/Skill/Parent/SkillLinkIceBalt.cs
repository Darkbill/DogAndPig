using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLinkIceBalt : Skill
{
    #region SkillSetting
    enum eSkillOption
    {
        Damage,
        CoolTime,
    }
    private float damage;
    public override void SkillSetting()
    {
        skillID = 10;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eSkillOption.CoolTime];
        delayTime = cooldownTime;
        gameObject.SetActive(false);
    }
    #endregion
    public LinkIce ice;

    //실제 쿨타임 도는 타이밍에 ActiveSkill();
    public override void OnButtonDown()
    {
        base.OnButtonDown();
        ActiveSkill();
    }
    public override void ActiveSkill()
    {
        ice.gameObject.SetActive(true);
        ice.Setting(skillID, 1000, damage);
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
        if (delayTime >= 5.0f)
            gameObject.SetActive(false);
    }
}
