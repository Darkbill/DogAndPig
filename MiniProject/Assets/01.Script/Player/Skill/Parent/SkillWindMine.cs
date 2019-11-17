using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindMine : Skill
{
    #region SkillSetting
    enum eWindMineOption
    {
        Damage,
        CoolTime,
        BombDamage,
        Radius,
    }
    //TODO : randrange는 해당 값의 /10하고 나오는 값.
    private float damage;
    private float bombDamage;
    private float radius;
    public override void SkillSetting()
    {
        skillID = 18;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        damage = skillData.optionArr[(int)eWindMineOption.Damage];
        cooldownTime = skillData.optionArr[(int)eWindMineOption.CoolTime];
        bombDamage = skillData.optionArr[(int)eWindMineOption.BombDamage];
        radius = skillData.optionArr[(int)eWindMineOption.Radius];
        delayTime = cooldownTime;
        gameObject.SetActive(false);
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
        }
    }
    public override void SetBullet()
    {
        foreach (WindMineSystem o in mine)
            o.Setting(damage, bombDamage, radius);
    }
    public override void OffSkill()
    {
        foreach (WindMineSystem o in mine)
            o.EndSystemSetting();
    }
    #endregion

    public List<WindMineSystem> mine = new List<WindMineSystem>();

    public override void OnButtonDown()
    {
        GameMng.Ins.SetSkillAim(skillID);
    }
    public override void OnDrop()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        CreateAndPool(mousePos);
        ActiveSkill();
    }
    public override void OnDrop(Vector2 pos)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(pos);
        CreateAndPool(mousePos);
        ActiveSkill();
    }
    private void CreateAndPool(Vector3 pos)
    {
        foreach(WindMineSystem m in mine)
        {
            if (m.gameObject.activeSelf) continue;
            m.SystemSetting(pos);
            return;
        }
        WindMineSystem o = Instantiate(mine[0], GameMng.Ins.skillMng.transform);
        o.Setting(damage, bombDamage, radius);
        o.SystemSetting(pos);
        mine.Add(o);
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
    }
}
