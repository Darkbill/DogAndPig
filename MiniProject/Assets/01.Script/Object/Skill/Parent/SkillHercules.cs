using GlobalDefine;
using UnityEngine;

public class SkillHercules : Skill
{
    #region SkillSetting
    enum eHerculesOption
    {
        Damage,
        BufTime,
        CoolTime,
		KnockBackPer,
    }
    private float damage;
    private float buftime;
	private float activePer;
    public GameObject HerculesEffect;
	
    public override void SkillSetting()
    {
        skillID = 4;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eHerculesOption.Damage];
        buftime= skillData.optionArr[(int)eHerculesOption.BufTime];
        cooldownTime = skillData.optionArr[(int)eHerculesOption.CoolTime];
		activePer = skillData.optionArr[(int)eHerculesOption.KnockBackPer];
		delayTime = cooldownTime;
    }
	public override void SetItemBuff(eSkillOption type, float changeValue)
	{
		switch (type)
		{
			case eSkillOption.Damage:
				damage += damage * changeValue;
				break;
			case eSkillOption.CoolTime:
				cooldownTime -= cooldownTime * changeValue;
				break;
			case eSkillOption.BuffActivePer:
				activePer += activePer * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{

	}
	#endregion
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		ActiveSkill();
	}
	public override void ActiveSkill()
    {
        base.ActiveSkill();
        GameMng.Ins.player.AddBuff(new ConditionData(eBuffType.PhysicsStrong, skillID, buftime, damage));
        GameMng.Ins.player.AddBuff(new ConditionData(eBuffType.NockBack, skillID, buftime, activePer));
        HerculesEffect.SetActive(true);
    }

    void Update()
    {
        HerculesEffect.gameObject.transform.position = GameMng.Ins.player.transform.position;
        delayTime += Time.deltaTime;
        if (delayTime >= buftime)
            HerculesEffect.gameObject.SetActive(false);
    }
}
