using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMucusAttack : Skill
{
    #region SkillSetting
    enum eOption
    {
        Damage,
        What,
        CoolDown,
        Radian
    }

    private float damage;
    private string decal;
    private float radian;
    public override void SkillSetting()
    {
        skillID = 2;
        MonsterSkillData skillData = JsonMng.Ins.monsterSkillDataTable[skillID];
        skillName = JsonMng.Ins.monsterSkillDataTable[skillID].skillName;
        decal = skillData.decal;
        damage = skillData.optionArr[(int)eOption.Damage];
        delayTime = cooldownTime;
        cooldownTime = skillData.optionArr[(int)eOption.CoolDown];
        radian = skillData.optionArr[(int)eOption.Radian];
        delayTime = cooldownTime;
        damage = skillData.optionArr[(int)eOption.Damage];
    }
    #endregion

    //public Meteor meteor;

    private const float setTime = 5.0f;

    private float bulletradius = 0.9f;

    public List<Mucus> mucusList = new List<Mucus>();
    public override void ActiveSkill()
    {
        base.ActiveSkill();
    }
    public override void SetBullet()
    {
    }
    public override void OffSkill()
    {
    }
    public override void SetItemBuff(eSkillOption optionType, float changeValue)
    {
    }
    private void Awake()
    {
        foreach (Mucus o in mucusList)
            o.Setting(skillID, setTime, damage, radian);
    }

    private void Update()
    {
    }

    public void SkillStart()
    {
        MucusRun();
    }

    private void MucusRun()
    {
        //점액질 active true
        foreach (Mucus o in mucusList)
        {
            if (!o.gameObject.activeSelf)
            {
                o.SystemSetting(GameMng.Ins.player.transform.position);
                return;
            }
        }
        Mucus m = Instantiate(mucusList[0]);
        m.Setting(skillID, setTime, damage, radian);
        m.SystemSetting(GameMng.Ins.player.transform.position);
        mucusList.Add(m);
    }
}
