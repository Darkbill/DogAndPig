using UnityEngine;
using System.Collections.Generic;
public class SkillFlameThrower : Skill
{
	public List<Flame> flameList;
	#region SkillSetting

	enum eFlameThrowerOption
	{
		Damage,
		CoolTime,
		ActiveTime,
		SpawnTime,
		FlameSpeed,
		UpScaleSpeed,
		FlameActiveTime,
		ReducationSpeed,
	}
	private float damage;
	private float activeTime;
	private float spawnTime;
	private float randSpawnTime;
	private float cTime;
	private float flameSpeed;
	private float upScaleSpeed;
	private float flameActiveTime;
	private float reducationSpeed;
	private const float updownScale = 0.1f;
	public override void SkillSetting()
	{
		skillID = 6;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eFlameThrowerOption.Damage];
		cooldownTime = skillData.optionArr[(int)eFlameThrowerOption.CoolTime];
		activeTime = skillData.optionArr[(int)eFlameThrowerOption.ActiveTime];
		spawnTime = skillData.optionArr[(int)eFlameThrowerOption.SpawnTime];
		flameSpeed = skillData.optionArr[(int)eFlameThrowerOption.FlameSpeed];
		upScaleSpeed = skillData.optionArr[(int)eFlameThrowerOption.UpScaleSpeed];
		flameActiveTime = skillData.optionArr[(int)eFlameThrowerOption.FlameActiveTime];
		reducationSpeed = skillData.optionArr[(int)eFlameThrowerOption.ReducationSpeed];
		randSpawnTime = spawnTime;
		delayTime = cooldownTime;
		for (int i = 0; i < flameList.Count; ++i)
		{
			flameList[i].Setting(skillType, damage, upScaleSpeed, flameActiveTime,reducationSpeed);
			flameList[i].transform.parent = GameMng.Ins.skillMng.transform;
		}
		gameObject.SetActive(false);
	}
	#endregion

	private void FixedUpdate()
	{
		delayTime += Time.deltaTime;
		if (activeFlag)
		{
			cTime += Time.deltaTime;
			if (delayTime > activeTime)
			{
				GameMng.Ins.EndSkillAim();
			}
			if (cTime >= randSpawnTime)
			{
				cTime -= randSpawnTime;
				randSpawnTime = Random.Range(spawnTime - spawnTime * updownScale, spawnTime + spawnTime * updownScale);
				CreateFlame();
			}
		}
	}
	private void CreateFlame()
	{
		for(int i = 0; i < flameList.Count; ++i)
		{
			if(flameList[i].gameObject.activeSelf == false)
			{
				Vector3 lookDir = GameMng.Ins.player.GetForward();
				lookDir = new Vector3(lookDir.x + Random.Range(-updownScale,updownScale), lookDir.y + Random.Range(-updownScale, updownScale));
				flameList[i].Setting(GameMng.Ins.player.transform.position,Random.Range(flameSpeed - flameSpeed * updownScale, flameSpeed + flameSpeed * updownScale), lookDir, GameMng.Ins.player.degree);
				return;
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
		ActiveSkill();
		activeFlag = true;
	}
	public override void OnDrop()
	{
		base.OnDrop();
		activeFlag = false;
	}
}
