using UnityEngine;
using GlobalDefine;
public class SkillLinkIceBalt : Skill
{
    #region SkillSetting
    enum eIceBaltSkillOption
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
	private float buffActivePer;
	private float buffEndTime;
	private float buffChangeValue;
	private eBuffType buffType;
	private float iceSpeed;
	private int maxHitCount;
	public override void SkillSetting()
    {
        skillID = 10;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eIceBaltSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eIceBaltSkillOption.CoolTime];
        delayTime = cooldownTime;
		buffActivePer = skillData.optionArr[(int)eIceBaltSkillOption.ActivePer];
		buffEndTime = skillData.optionArr[(int)eIceBaltSkillOption.EndTime];
		buffChangeValue = skillData.optionArr[(int)eIceBaltSkillOption.ChangeValue];
		buffType = (eBuffType)skillData.optionArr[(int)eIceBaltSkillOption.BuffType];
		iceSpeed = skillData.optionArr[(int)eIceBaltSkillOption.IceSpeed];
		maxHitCount = (int)skillData.optionArr[(int)eIceBaltSkillOption.MaxHitCount];
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
			case eSkillOption.BuffActivePer:
				buffActivePer += buffActivePer * changeValue;
				break;
			case eSkillOption.BuffEndTime:
				buffEndTime += buffEndTime * changeValue;
				break;
			case eSkillOption.BuffChangeValue:
				changeValue += changeValue * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		ice.Setting(skillID, buffActivePer, damage, skillType,
			buffType, buffEndTime, buffChangeValue,
			iceSpeed,
			maxHitCount);
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
