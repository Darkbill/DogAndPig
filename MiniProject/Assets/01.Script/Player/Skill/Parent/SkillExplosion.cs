using UnityEngine;

public class SkillExplosion : Skill
{
	public ExplosionFire explosionFire;
	#region SkillSetting
	enum eSkillOption
	{
		Damage,
		CoolTime,
	}
	private float damage;
	public override void SkillSetting()
	{
		skillID = 9;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eSkillOption.CoolTime];
		delayTime = cooldownTime;
		explosionFire.Setting(skillType, damage,skillID);
		gameObject.SetActive(false);
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
		Explosion();
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}
	public void Explosion()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		explosionFire.StartExPlosion(mousePos);
	}
}