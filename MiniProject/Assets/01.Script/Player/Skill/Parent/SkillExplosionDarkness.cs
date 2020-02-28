using UnityEngine;
using GlobalDefine;

public class SkillExplosionDarkness : Skill
{
	#region SkillSetting
	enum eDarkSkillOption
	{
		Damage,
		CoolTime,
		MaxCharageTime,
		ChargeSpeed,
	}
	private float damage;
	private float maxChargeTime;
	private float chargeSpeed;
	public override void SkillSetting()
	{
		skillID = 12;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eDarkSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eDarkSkillOption.CoolTime];
		maxChargeTime = skillData.optionArr[(int)eDarkSkillOption.MaxCharageTime];
		chargeSpeed = skillData.optionArr[(int)eDarkSkillOption.ChargeSpeed];
		delayTime = cooldownTime;
		explosiondarkness.transform.parent = GameMng.Ins.skillMng.transform;
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
			case eSkillOption.Speed:
				chargeSpeed += chargeSpeed * changeValue;
				break;
            case eSkillOption.BuffActivePer:
                maxChargeTime += maxChargeTime * changeValue;
                break;
        }
	}
	public override void SetBullet()
	{
		explosiondarkness.Setting(skillID, damage, maxChargeTime, chargeSpeed);
	}
    public override void OffSkill()
    {
        explosiondarkness.gameObject.SetActive(false);
    }

    #endregion

    public ExplosionDarkness explosiondarkness;

	private float chargetime = 0.0f;

	public override void OnButtonDown()
	{
		GameMng.Ins.SetSkillAim(skillID);
	}
	public override void OnDrag()
	{
		base.OnDrag();
        explosiondarkness.SubSystemSetting(true);
    }
	public override void OnDrop()
	{
		base.OnDrop();
        explosiondarkness.SystemSetting(GameMng.Ins.player.transform.position +
            new Vector3(0, GameMng.Ins.player.calStat.size));

		base.ActiveSkill();
	}

    public override void OnDrop(Vector2 pos)
    {
        base.OnDrop(pos);
        explosiondarkness.SystemSetting(GameMng.Ins.player.transform.position +
            new Vector3(0, GameMng.Ins.player.calStat.size));

        base.ActiveSkill();
    }
    private void Update()
	{
		delayTime += Time.deltaTime;
	}
}
