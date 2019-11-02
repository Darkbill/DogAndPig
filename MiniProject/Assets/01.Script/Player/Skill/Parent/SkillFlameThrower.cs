using UnityEngine;

public class SkillFlameThrower : Skill
{
	public Flame flame;
	private bool activeFlag;
	#region SkillSetting
	private float activeTime;
	enum eFrameThrowerOption
	{
		Damage,
		CoolTime,
		ActiveTime,
	}
	private float damage;

	public override void SkillSetting()
	{
		skillID = 6;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eFrameThrowerOption.Damage];
		cooldownTime = skillData.optionArr[(int)eFrameThrowerOption.CoolTime];
		activeTime = skillData.optionArr[(int)eFrameThrowerOption.ActiveTime];
		delayTime = cooldownTime;
		flame.Setting(skillType, damage);
		gameObject.SetActive(false);
	}
	#endregion
	void Update()
	{
		delayTime += Time.deltaTime;
		if (activeFlag)
		{
			if (delayTime > activeTime)
			{
				flame.gameObject.SetActive(false);
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
		flame.gameObject.SetActive(false);
	}
}
