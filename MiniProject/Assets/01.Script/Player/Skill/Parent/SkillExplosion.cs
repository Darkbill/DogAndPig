using UnityEngine;
using GlobalDefine;
using System.Collections.Generic;
public class SkillExplosion : Skill
{
	public List<ExplosionFire> explosionFireList = new List<ExplosionFire>();

	#region SkillSetting
	enum eExplosionSkillOption
	{
		Damage,
		CoolTime,
		ThrowTime,
		UpScale,
		KnockBackPower,
	}
	private float damage;
	private float throwTime;
	private float upScale;
	private float knockBackPower;
	public override void SkillSetting()
	{
		skillID = 9;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eExplosionSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eExplosionSkillOption.CoolTime];
		throwTime = skillData.optionArr[(int)eExplosionSkillOption.ThrowTime];
		upScale = skillData.optionArr[(int)eExplosionSkillOption.UpScale];
		knockBackPower = skillData.optionArr[(int)eExplosionSkillOption.KnockBackPower];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
	}
	public override void SetItemBuff(eSkillOption optionType, float changeValue)
	{
		switch (optionType)
		{
			case eSkillOption.Damage:
				damage += damage * changeValue;
				break;
			case eSkillOption.CoolTime:
				cooldownTime -= cooldownTime * changeValue;
				break;
			case eSkillOption.BuffChangeValue:
				knockBackPower += knockBackPower * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		for (int i = 0; i < explosionFireList.Count; ++i)
		{
			explosionFireList[i].Setting(skillType, damage, skillID, knockBackPower,throwTime,upScale);
		}
	}
	public override void OffSkill()
	{
		for (int i = 0; i < explosionFireList.Count; ++i)
		{
			explosionFireList[i].ActiveOff();
		}
	}
	#endregion
	//실제 쿨타임 도는 타이밍에 ActiveSkill();
	public override void OnButtonDown()
	{
		base.OnButtonDown();
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
		GetActiveAbleExplosion().StartThrow();
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}

	private ExplosionFire GetActiveAbleExplosion()
	{
		for (int i = 0; i < explosionFireList.Count; ++i)
		{
			if (explosionFireList[i].gameObject.activeSelf == false)
			{
				return explosionFireList[i];
			}
		}
		GameObject o = Instantiate(explosionFireList[0].gameObject);
		o.GetComponent<ExplosionFire>().Setting(skillType, damage, skillID, knockBackPower, throwTime, upScale);
		explosionFireList.Add(o.GetComponent<ExplosionFire>());
		return explosionFireList[explosionFireList.Count - 1];
	}
}