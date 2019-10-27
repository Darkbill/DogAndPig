using UnityEngine;

public class SkillFloorFreeze : Skill
{
	#region SkillSetting
	enum eFloorFreezeOption
    {
        Damage,
		Width,
		Height,
		CoolTime,
		DebufIndex,
		DebufActivePer,
		DebugEffectPer,
		DebufTime,
    }
	private float damage;
	private float width;
	private float height;
	private float DebufIndex;
	private float DebufActivePer;
	private float DebufEffectPer;
	private float DebufTime;

	public override void SkillSetting()
	{
		skillID = 3;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eFloorFreezeOption.Damage];
		DebufIndex = skillData.optionArr[(int)eFloorFreezeOption.DebufIndex];
		DebufActivePer = skillData.optionArr[(int)eFloorFreezeOption.DebufActivePer];
		DebufTime = skillData.optionArr[(int)eFloorFreezeOption.DebufTime];
		width = skillData.optionArr[(int)eFloorFreezeOption.Width];
		height = skillData.optionArr[(int)eFloorFreezeOption.Height];
		cooldownTime = skillData.optionArr[(int)eFloorFreezeOption.CoolTime];
		DebufEffectPer = skillData.optionArr[(int)eFloorFreezeOption.DebugEffectPer];
		delayTime = cooldownTime;
        FreezenShot.Setting(skillID, DebufTime, 0.5f, DebufActivePer);
		gameObject.SetActive(false);
	}
	#endregion

	//TODO : SpriteDummy
	public Freezen FreezenShot;
	public bool isAim = false;
    public override bool ActiveSkill()
    {
		if (isAim == false)
		{
			StartAim();
			isAim = true;
			return false;
		}
		else
		{
			isAim = false;
			base.ActiveSkill();
			float degree = GameMng.Ins.player.degree;
			Vector3 pos = new Vector3(Mathf.Cos(degree), Mathf.Sin(degree), 0);
			FreezenShot.gameObject.SetActive(true);
			FreezenShot.transform.position = GameMng.Ins.player.transform.position;
			FreezenShot.transform.eulerAngles = new Vector3(0, 0, degree + 90);
			FreezenShot.transform.localScale = new Vector3(width, height / 2, 0);
			return true;
		}
    }
	public override void OffAim()
	{
		isAim = false;
	}
	public void StartAim()
	{
		GameMng.Ins.SetSkillAim(skillID);
	}
    void Update()
    {
		delayTime += Time.deltaTime;
		if (delayTime >= 0.5f)
            FreezenShot.gameObject.SetActive(false);
    }
}
