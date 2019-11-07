using UnityEngine;
using GlobalDefine;

public class SkillRoundTrip : Skill
{
    #region SkillSetting
    enum eTripSkillOption
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
        damage = skillData.optionArr[(int)eTripSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eTripSkillOption.CoolTime];
        range = skillData.optionArr[(int)eTripSkillOption.Range];
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
				range += range * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{

	}
	#endregion

	public Recognition recognition;
    //실제 쿨타임 도는 타이밍에 ActiveSkill();
    public override void OnButtonDown()
    {
        GameMng.Ins.SetSkillAim(skillID);
    }
    //public override void ActiveSkill()
    //{
    //    base.ActiveSkill();
    //}
    public override void OnDrag()
    {
        base.OnDrag();
    }
    public override void OnDrop()
    {
        base.OnDrop();
        ActiveSkill();
        Vector3 movevec = GameMng.Ins.player.GetForward();
        recognition.gameObject.SetActive(true);
        recognition.Setting(GameMng.Ins.player.transform.position, movevec, range);
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
    }
}
