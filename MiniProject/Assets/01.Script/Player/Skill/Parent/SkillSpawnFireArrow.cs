using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefine;
public class SkillSpawnFireArrow : Skill
{
	public ParticleSystem gateParticle;
	public List<FireArrow> firArrowList;
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
		skillType = skillData.skillType;
		target = skillData.target;
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
		for (int i = 0; i < firArrowList.Count; ++i)
		{
			firArrowList[i].Setting(skillType, damage, arrowSpeed, arrowActiveTime);
			firArrowList[i].transform.parent = GameMng.Ins.skillMng.transform;
		}
	}
	public override void OffSkill()
	{
		for (int i = 0; i < firArrowList.Count; ++i)
		{
			firArrowList[i].gameObject.SetActive(false);
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
		StartCoroutine(CreateArrow());
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}
	private IEnumerator CreateArrow()
	{
		float cTime = 0;
		while (delayTime <= activeTime)
		{
			if (gameObject.activeSelf == false) yield break;
			cTime += Time.deltaTime;
			if (cTime >= arrowInitTime)
			{
				cTime -= arrowInitTime;
				for (int i = 0; i < firArrowList.Count; ++i)
				{
					if (firArrowList[i].gameObject.activeSelf == false)
					{
						firArrowList[i].Setting(gateParticle.gameObject.transform.right, gateParticle.gameObject.transform.position, gateParticle.gameObject.transform.eulerAngles.z);
						break;
					}
				}
				FireArrow o = Instantiate(firArrowList[0], GameMng.Ins.skillMng.transform);
				firArrowList.Add(o);
				o.Setting(skillType, damage, arrowSpeed, arrowActiveTime);
				o.Setting(gateParticle.gameObject.transform.right, gateParticle.gameObject.transform.position, gateParticle.gameObject.transform.eulerAngles.z);
			}
			yield return null;
		}
	}
	private void CreateGate()
	{
		gateParticle.gameObject.SetActive(true);
		gateParticle.gameObject.transform.position = GameMng.Ins.player.transform.position;
		gateParticle.gameObject.transform.eulerAngles = new Vector3(0, 0, GameMng.Ins.player.degree);
		gateParticle.Play();
	}
}

