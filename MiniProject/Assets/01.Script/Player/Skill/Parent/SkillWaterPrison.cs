using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;

public class SkillWaterPrison : Skill
{
    #region SkillSetting
    enum eTripSkillOption
    {
        Damage,
        CoolTime,
    }
    private float damage;
    public override void SkillSetting()
    {
        skillID = 14;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        damage = skillData.optionArr[(int)eTripSkillOption.Damage];
        cooldownTime = skillData.optionArr[(int)eTripSkillOption.CoolTime];
        delayTime = cooldownTime;
        gameObject.transform.parent = GameMng.Ins.skillMng.transform;
        gameObject.SetActive(false);
        waterlist[0].transform.parent = GameMng.Ins.skillMng.transform;
    }
    public override void SetItemBuff(eSkillOption optionType, float changeValue)
    {
        switch (optionType)
        {
            case eSkillOption.Damage:
                damage += damage * changeValue;
                break;
        }
    }
    public override void SetBullet()
    {
        foreach(WaterBall o in waterlist)
            o.Setting(skillID, damage);
    }
	public override void OffSkill()
	{
        foreach (WaterBall o in waterlist)
        {
            o.gameObject.SetActive(false);
            o.hit.gameObject.SetActive(false);
            o.prison.gameObject.SetActive(false);
        }
	}
	#endregion
	//TODO : 성민형 과제
	public List<WaterBall> waterlist = new List<WaterBall>();
    //실제 쿨타임 도는 타이밍에 ActiveSkill();
    public override void OnButtonDown()
    {
        GameMng.Ins.SetSkillAim(skillID);
    }
    public override void OnDrag()
    {
        base.OnDrag();
    }
    public override void OnDrop()
    {
        base.OnDrop();
        ActiveSkill();
        WaterSet();
    }

	private void WaterSet()
    {
        Vector3 movevec = GameMng.Ins.player.GetForward();
        for (int i = 0; i < waterlist.Count; ++i)
        {
            if (waterlist[i].gameObject.activeSelf) continue;
            waterlist[i].gameObject.SetActive(true);
            waterlist[i].SystemSetting(movevec);

            return;
        }
        WaterBall o = Instantiate(waterlist[0], GameMng.Ins.skillMng.transform);
        o.gameObject.SetActive(true);
        o.Setting(skillID, damage);
        o.SystemSetting(movevec);
        waterlist.Add(o);
    }
    private void Update()
    {
        delayTime += Time.deltaTime;
    }
}
