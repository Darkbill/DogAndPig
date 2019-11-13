using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class SkillCircleShot : Skill
{
	#region SkillSetting
	enum eCircleShotOption
	{
		Damage,
		EndTimer,
		CoolTime,
		fireSpeed,
		fireDuration,
	}
	private float damage;
	private float endTime;//AllTimer
	private float fireSpeed;
	private float duration;
	public override void SkillSetting()
	{
		skillID = 2;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eCircleShotOption.Damage];
		endTime = skillData.optionArr[(int)eCircleShotOption.EndTimer];
		cooldownTime = skillData.optionArr[(int)eCircleShotOption.CoolTime];
		fireSpeed = skillData.optionArr[(int)eCircleShotOption.fireSpeed];
		duration = skillData.optionArr[(int)eCircleShotOption.fireDuration];
		BulletSetting();
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
			case eSkillOption.Speed:
				fireSpeed += fireSpeed * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			BulletLst[i].Setting(damage, fireSpeed, endTime, duration);	
		}
	}
	public override void OffSkill()
	{
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			BulletLst[i].gameObject.SetActive(false);
		}
	}
	#endregion
	const int Angle180 = 180;
	const float Radius = 1.5f;
	public List<FireBall> BulletLst = new List<FireBall>();
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		ActiveSkill();
	}
	public override void ActiveSkill()
	{
		base.ActiveSkill();
		gameObject.transform.position = GameMng.Ins.player.transform.position;
		gameObject.transform.eulerAngles = Vector3.zero;
		BulletSetting();
	}
	private void BulletSetting()
	{
		Vector3 bulletstartpos = new Vector3(Radius, 0, 0);
		Vector3 bulletstartvec = new Vector3(0, Radius, 0);
		int count = 0;
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			Quaternion radian = Quaternion.Euler(0, 0, Angle180 * 2 / 5 * count);
			if (count == 5) break;
			if (!BulletLst[i].gameObject.activeSelf)
			{
				Vector3 pos = radian * bulletstartpos +
					GameMng.Ins.player.transform.position;
				Vector3 moveVec = radian * bulletstartvec;
				BulletLst[i].SystemSetting(pos, moveVec, new Vector3(30, 0, -90));
				++count;
				continue;
			}
			if (i + 1 == BulletLst.Count)
			{
				FireBall o = Instantiate(BulletLst[0], GameMng.Ins.skillMng.transform);
				Vector3 pos = radian * bulletstartpos +
					GameMng.Ins.player.transform.position;
				Vector3 moveVec = radian * bulletstartvec;
				o.Setting(damage, fireSpeed, endTime, duration);
				o.SystemSetting(pos, moveVec, new Vector3(30, 0, -90));
				BulletLst.Add(o);
				++count;
			}

		}
	}

	private void FixedUpdate()
	{
		delayTime += Time.deltaTime;
	}
}
