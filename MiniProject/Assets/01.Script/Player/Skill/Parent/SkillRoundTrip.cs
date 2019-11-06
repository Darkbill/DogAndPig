using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRoundTrip : Skill
{
    #region SkillSetting
    enum eSkillOption
    {
        Damage,
        CoolTime,
        Range,
    }
    private float damage;
    private float range;
    public override void SkillSetting()
    {
        skillID = 11;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eSkillOption.CoolTime];
        range = skillData.optionArr[(int)eSkillOption.Range];
        delayTime = cooldownTime;
        gameObject.SetActive(false);
    }
    #endregion

    public Recognition recognition;
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
        base.OnDrop();
        ActiveSkill();
        float degree = GameMng.Ins.player.degree;
        Vector3 movevec = new Vector3(Mathf.Cos(degree), Mathf.Sin(degree));
        recognition.Setting(GameMng.Ins.transform.position + movevec, movevec, range);
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
    }
}
