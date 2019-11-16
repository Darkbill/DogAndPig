using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillThounderCalling : Skill
{
	#region SkillSetting
	enum eThoumderOption
	{
		Damage,
		CoolTime,
		MaxCount,
		RandRange,
	}
	//TODO : randrange는 해당 값의 /10하고 나오는 값.
	private float damage;
	private float maxcount;
	private int randrange;
	public override void SkillSetting()
	{
		skillID = 13;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eThoumderOption.Damage];
		cooldownTime = skillData.optionArr[(int)eThoumderOption.CoolTime];
		maxcount = skillData.optionArr[(int)eThoumderOption.MaxCount];
		randrange = (int)skillData.optionArr[(int)eThoumderOption.RandRange];
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
        foreach (ThounderSystem o in thoundersystem)
            o.Setting(damage, randrange, maxcount);
	}
	public override void OffSkill()
	{
        foreach (ThounderSystem o in thoundersystem)
            o.gameObject.SetActive(false);
	}
    #endregion

    public List<ThounderSystem> thoundersystem = new List<ThounderSystem>();

	public override void OnButtonDown()
	{
		GameMng.Ins.SetSkillAim(skillID);
	}
	public override void OnDrop()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach(ThounderSystem o in thoundersystem)
        {
            o.SystemSetting(mousePos);
            ActiveSkill();
            return;
        }
        ThounderSystem clone = Instantiate(thoundersystem[0], GameMng.Ins.skillMng.transform);
        clone.Setting(damage, randrange, maxcount);
        clone.SystemSetting(mousePos);
        thoundersystem.Add(clone);
        ActiveSkill();
    }
    public override void OnDrop(Vector2 pos)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(pos);
		mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        foreach (ThounderSystem o in thoundersystem)
        {
            o.SystemSetting(mousePos);
            ActiveSkill();
            return;
        }
        ThounderSystem clone = Instantiate(thoundersystem[0], GameMng.Ins.skillMng.transform);
        clone.Setting(damage, randrange, maxcount);
        clone.SystemSetting(mousePos);
        thoundersystem.Add(clone);
        ActiveSkill();
    }
    private void Update()
	{
		delayTime += Time.deltaTime;
	}
	//TODO : 성민형 과제
}
