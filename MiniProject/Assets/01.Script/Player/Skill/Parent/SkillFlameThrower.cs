using UnityEngine;

public class SkillFlameThrower : Skill
{
	public Flame flame;
	#region SkillSetting
	private float activeTime;
	enum eFrameThrowerOption
	{
		Damage,
		CoolTime,
		ActiveTime,
	}
	private float damage;

	public float ActiveTime { get => activeTime; set => activeTime = value; }
	public float Damage { get => damage; set => damage = value; }

	public override void SkillSetting()
	{
		skillID = 6;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		Damage = skillData.optionArr[(int)eFrameThrowerOption.Damage];
		cooldownTime = skillData.optionArr[(int)eFrameThrowerOption.CoolTime];
		ActiveTime = skillData.optionArr[(int)eFrameThrowerOption.ActiveTime];
		delayTime = cooldownTime;
		flame.Setting(skillType, Damage);
		gameObject.SetActive(false);
	}
	#endregion
	void Update()
	{
		delayTime += Time.deltaTime;
		if (activeFlag)
		{
			if (delayTime > ActiveTime)
			{
				GameMng.Ins.EndSkillAim();
			}
		}
	}
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		GameMng.Ins.SetSkillAim(skillID);
	}
	public override void OnDrag()
	{
		base.OnDrag();
		UIMngInGame.Ins.CoolDownAllSkill();
		ActiveSkill();
		flame.gameObject.SetActive(true);
		activeFlag = true;
	}
	public override void OnDrop()
	{
		base.OnDrop();
		activeFlag = false;
		flame.gameObject.SetActive(false);
	}
}
