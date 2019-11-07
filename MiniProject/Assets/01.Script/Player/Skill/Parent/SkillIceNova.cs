using UnityEngine;
using GlobalDefine;

public class SkillIceNova : Skill
{
    #region SkillSetting
    enum eNovaSkillOption
    {
        Damage,
        CoolTime,
		DebufPer,
		SlowPer,
		DebufEndTime,
    }
    private float damage;
	private float debufPer;
	private float slowPer;
	private float debufEndTime;
    public override void SkillSetting()
    {
        skillID = 8;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eNovaSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eNovaSkillOption.CoolTime];
        delayTime = cooldownTime;
		debufPer = skillData.optionArr[(int)eNovaSkillOption.DebufPer];
		slowPer = skillData.optionArr[(int)eNovaSkillOption.SlowPer];
		debufEndTime = skillData.optionArr[(int)eNovaSkillOption.DebufEndTime];
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
		}
	}
	public override void SetBullet()
	{

	}
	#endregion

	public Nova nova;
    //실제 쿨타임 도는 타이밍에 ActiveSkill();
    public override void OnButtonDown()
    {
        base.OnButtonDown();
        ActiveSkill();
    }
    public override void ActiveSkill()
    {
        nova.Setting(skillID, debufPer /* slow per*/, damage,slowPer,debufEndTime);
        gameObject.transform.position = GameMng.Ins.player.transform.position +
            new Vector3(0, GameMng.Ins.player.calStat.size * 3);
        nova.transform.position = GameMng.Ins.player.transform.position;
        nova.gameObject.SetActive(true);
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
        moveset();
    }

    private void moveset()
    {
        gameObject.transform.position = GameMng.Ins.player.transform.position +
            new Vector3(0, GameMng.Ins.player.calStat.size * 3);
        nova.transform.position = GameMng.Ins.player.transform.position;
    }
}
