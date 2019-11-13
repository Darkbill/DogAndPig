using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefine;
public class SkillSpawnFireArrow : Skill
{
	public List<FireArrowGate> gateParticleList = new List<FireArrowGate>();

	#region SkillSetting
	enum eSpawnFireArrowOption
	{
		Damage,
		CoolTime,
		ActiveTime,
		ArrowInitTime,
		ArrowSpeed,
		ArrowActiveTime,
	}
	private float damage;
	private float activeTime;
	private float arrowInitTime;
	private float arrowSpeed;
	private float arrowActiveTime;
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
			case eSkillOption.SpawnDelay:
				arrowInitTime += arrowInitTime * changeValue;
				break;
			case eSkillOption.Speed:
				arrowSpeed += arrowSpeed * changeValue;
				break;
		}
	}
	public override void SkillSetting()
	{
		skillID = 7;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eSpawnFireArrowOption.Damage];
		cooldownTime = skillData.optionArr[(int)eSpawnFireArrowOption.CoolTime];
		activeTime = skillData.optionArr[(int)eSpawnFireArrowOption.ActiveTime];
		arrowInitTime = skillData.optionArr[(int)eSpawnFireArrowOption.ArrowInitTime];
		arrowSpeed = skillData.optionArr[(int)eSpawnFireArrowOption.ArrowSpeed];
		arrowActiveTime = skillData.optionArr[(int)eSpawnFireArrowOption.ArrowActiveTime];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
	}
	public override void SetBullet()
	{
		for (int i = 0; i < gateParticleList.Count; ++i)
		{
			gateParticleList[i].Setting(activeTime, arrowInitTime,damage,arrowSpeed,arrowActiveTime);
		}
	}
	public override void OffSkill()
	{
		for (int i = 0; i < gateParticleList.Count; ++i)
		{
			gateParticleList[i].OffSkill();
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
		CreateGate();
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}

	private void CreateGate()
	{
		GetAciveAbleGate().StartGate();
	}
	private FireArrowGate GetAciveAbleGate()
	{
		for(int i = 0; i < gateParticleList.Count; ++i)
		{
			if(gateParticleList[i].gameObject.activeSelf == false)
			{
				return gateParticleList[i];
			}
		}
		GameObject o = Instantiate(gateParticleList[0].gameObject);
		o.GetComponent<FireArrowGate>().Setting(activeTime, arrowInitTime, damage, arrowSpeed, arrowActiveTime);
		gateParticleList.Add(o.GetComponent<FireArrowGate>());
		return gateParticleList[gateParticleList.Count - 1];
	}

}

