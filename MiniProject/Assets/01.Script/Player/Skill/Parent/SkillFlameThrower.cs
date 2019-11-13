using UnityEngine;
using System.Collections.Generic;
using GlobalDefine;
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
		DropSpeed,
	}
	private float damage;
	private float activeTime;
	private float spawnTime;
	private float randSpawnTime;
	private float cTime;
	private float flameSpeed;
	private float upScaleSpeed;
	private float flameActiveTime;
	private float dropSpeed;
	private const float updownScale = 0.1f;
	public override void SkillSetting()
	{
		skillID = 6;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eFlameThrowerOption.Damage];
		cooldownTime = skillData.optionArr[(int)eFlameThrowerOption.CoolTime];
		activeTime = skillData.optionArr[(int)eFlameThrowerOption.ActiveTime];
		spawnTime = skillData.optionArr[(int)eFlameThrowerOption.SpawnTime];
		flameSpeed = skillData.optionArr[(int)eFlameThrowerOption.FlameSpeed];
		upScaleSpeed = skillData.optionArr[(int)eFlameThrowerOption.UpScaleSpeed];
		flameActiveTime = skillData.optionArr[(int)eFlameThrowerOption.FlameActiveTime];
		dropSpeed = skillData.optionArr[(int)eFlameThrowerOption.DropSpeed];
		randSpawnTime = spawnTime;
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
			case eSkillOption.ActiveTime:
				activeTime += activeTime * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		for (int i = 0; i < flameList.Count; ++i)
		{
			flameList[i].Setting(damage, upScaleSpeed, flameActiveTime, dropSpeed);
			flameList[i].transform.parent = GameMng.Ins.skillMng.transform;
		}
	}
	public override void OffSkill()
	{
		for (int i = 0; i < flameList.Count; ++i)
		{
			flameList[i].gameObject.SetActive(false);
		}
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
		for (int i = 0; i < flameList.Count; ++i)
		{
			if (flameList[i].gameObject.activeSelf == false)
			{
				Vector3 lookDir = GameMng.Ins.player.GetForward();
				lookDir = new Vector3(lookDir.x + Random.Range(-updownScale, updownScale), lookDir.y + Random.Range(-updownScale, updownScale));
				flameList[i].Setting(GameMng.Ins.player.transform.position, Random.Range(flameSpeed - flameSpeed * updownScale, flameSpeed + flameSpeed * updownScale), lookDir, GameMng.Ins.player.degree);
				return;
			}
		}
		Flame o = Instantiate(flameList[0], GameMng.Ins.skillMng.transform);
			flameList.Add(o);
			o.Setting(damage, upScaleSpeed, flameActiveTime, dropSpeed);
			o.Setting(GameMng.Ins.player.transform.position, Random.Range(flameSpeed - flameSpeed * updownScale, flameSpeed + flameSpeed * updownScale), GameMng.Ins.player.GetForward(), GameMng.Ins.player.degree);
		
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
