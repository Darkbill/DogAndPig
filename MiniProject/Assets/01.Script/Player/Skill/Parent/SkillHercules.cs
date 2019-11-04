using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHercules : Skill
{
    #region SkillSetting
    enum eHerculesOption
    {
        BufIndex,
        Damage,
        BufTime,
        AttackPer,
        AttackRange,
        ColTime,
    }
    private float bufindex;
    private float damage;
    private float buftime;
    private float attackper;
    private float attackrange;

    public GameObject HerculesEffect;

    public override void SkillSetting()
    {
        skillID = 4;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];


        skillType = skillData.skillType;
        target = skillData.target;

        bufindex = skillData.optionArr[(int)eHerculesOption.BufIndex];
        damage = skillData.optionArr[(int)eHerculesOption.Damage];
        buftime= skillData.optionArr[(int)eHerculesOption.BufTime];
        attackper = skillData.optionArr[(int)eHerculesOption.AttackPer];
        attackrange = skillData.optionArr[(int)eHerculesOption.AttackRange];
        cooldownTime = skillData.optionArr[(int)eHerculesOption.ColTime];
        delayTime = cooldownTime;
    }
	#endregion
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		ActiveSkill();
		UIMngInGame.Ins.CoolDownAllSkill();
	}
	public override void ActiveSkill()
    {
        base.ActiveSkill();
        GameMng.Ins.player.AddBuff(new ConditionData(eBuffType.PhysicsStrong, skillID, buftime, damage));
        GameMng.Ins.player.AddBuff(new ConditionData(eBuffType.NockBack, skillID, buftime, 1000));
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
