using UnityEngine;

public class SkillIceNova : Skill
{
    #region SkillSetting
    enum eSkillOption
    {
        Damage,
        CoolTime,
    }
    private float damage;
    public override void SkillSetting()
    {
        skillID = 8;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eSkillOption.CoolTime];
        delayTime = cooldownTime;
        gameObject.SetActive(false);
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
        nova.Setting(skillID, 1000 /* slow per*/, damage);
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
        if (delayTime >= 5.0f)
            gameObject.SetActive(false);
        moveset();
    }

    private void moveset()
    {
        gameObject.transform.position = GameMng.Ins.player.transform.position +
            new Vector3(0, GameMng.Ins.player.calStat.size * 3);
        nova.transform.position = GameMng.Ins.player.transform.position;
    }
}
