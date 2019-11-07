using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExplosionDarkness : Skill
{
    #region SkillSetting
    enum eSkillOption
    {
        Damage,
        CoolTime,
        MaxCharageTime,
    }
    private float damage;
    private float maxChargeTime;
    private bool DragStart = false;
    public override void SkillSetting()
    {
        skillID = 12;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eSkillOption.CoolTime];
        maxChargeTime = skillData.optionArr[(int)eSkillOption.MaxCharageTime];
        delayTime = cooldownTime;
        gameObject.SetActive(false);
    }
    #endregion

    public ExplosionDarkness explosiondarkness;
    public GameObject charge;

    private float chargetime = 0.0f;

    public override void OnButtonDown()
    {
        GameMng.Ins.SetSkillAim(skillID);
        chargetime = 0.0f;
        ActiveSkill();
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
    }
    public override void OnDrag()
    {
        base.OnDrag();
        DragStart = true;
        charge.GetComponent<ParticleSystem>().Play();
    }
    public override void OnDrop()
    {
        base.OnDrop();
        charge.GetComponent<ParticleSystem>().Stop();
        DragStart = false;
        if (chargetime < 1)
            chargetime = 1.0f;
        explosiondarkness.gameObject.transform.position = GameMng.Ins.player.transform.position + 
            new Vector3(0, GameMng.Ins.player.calStat.size);
        explosiondarkness.gameObject.SetActive(true);
        explosiondarkness.Setting(skillID, chargetime, damage);
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
        if(DragStart)
        {
            if (chargetime < maxChargeTime)
                chargetime += Time.deltaTime;
            charge.transform.position = GameMng.Ins.player.transform.position;
        }
    }
}
