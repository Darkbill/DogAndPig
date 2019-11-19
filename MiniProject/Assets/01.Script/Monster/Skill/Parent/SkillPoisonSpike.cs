using System.Collections;
using System.Collections.Generic;
using GlobalDefine;
using UnityEngine;

public class SkillPoisonSpike : Skill
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
    private const float degree = 45;
    private const float speed = 2;
    private float[] settingArray = new float[] {-30, 0, 30, 150, 180, 210 };

    private float bulletradius = 0.9f;

    public List<PoisonSpike> spike = new List<PoisonSpike>();
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

    private void Awake()
    {
        for (int i = 0; i < spike.Count; ++i)
            spike[i].Setting(skillID, setTime, damage, radian, 1, speed);
    }

    private void Update()
    {
    }

    public void SkillStart(Monster monster)
    {
        SpikeRun(monster);
    }

    private void SpikeRun(Monster monster)
    {
        int count = 0;
        for(int i = 0;i<spike.Count;++i)
        {
            if (count >= 6) return;
            if (!spike[i].gameObject.activeSelf)
            {
                spike[i].SystemSetting(settingArray[count], monster);
                ++count;
            }
        }
        for(int i = 0;i< 6 - count;++i)
        {
            if (count >= 6) return;
            PoisonSpike o = Instantiate(spike[0]);
            o.Setting(skillID, setTime, damage, radian, settingArray[count], speed);
            ++count;
        }
    }

    public override void SetItemBuff(eSkillOption optionType, float changeValue)
    {
        throw new System.NotImplementedException();
    }
}
