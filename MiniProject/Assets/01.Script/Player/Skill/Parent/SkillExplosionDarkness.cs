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
	private bool DragStart = false;
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
		}
	}
	public override void SetBullet()
	{
		explosiondarkness.Setting(skillID, chargetime, damage);
	}
	#endregion

	public ExplosionDarkness explosiondarkness;
	public GameObject charge;

	private float chargetime = 0.0f;

	public override void OnButtonDown()
	{
		GameMng.Ins.SetSkillAim(skillID);
		chargetime = 0.0f;
	}
	public override void OffSkill()
	{
		explosiondarkness.gameObject.SetActive(false);
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
		ActiveSkill();
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
		if (DragStart)
		{
			if (chargetime < maxChargeTime)
				chargetime += Time.deltaTime * chargeSpeed;
			charge.transform.position = GameMng.Ins.player.transform.position;
		}
	}
}
