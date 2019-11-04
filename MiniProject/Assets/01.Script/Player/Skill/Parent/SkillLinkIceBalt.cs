using UnityEngine;
using GlobalDefine;
public class SkillLinkIceBalt : Skill
{
    #region SkillSetting
    enum eSkillOption
    {
        Damage,
        CoolTime,
		ActivePer,
		BuffType,
		EndTime,
		ChangeValue,
		IceSpeed,
		MaxHitCount,
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
		ice.Setting(skillID, skillData.optionArr[(int)eSkillOption.ActivePer],damage,skillType,
					(eBuffType)skillData.optionArr[(int)eSkillOption.BuffType], skillData.optionArr[(int)eSkillOption.EndTime],
					skillData.optionArr[(int)eSkillOption.ChangeValue], skillData.optionArr[(int)eSkillOption.IceSpeed],
					(int)skillData.optionArr[(int)eSkillOption.MaxHitCount]);

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
        ice.Setting();
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
    }
}
