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
		CoolTime
	}
	private float damage = 0;//Damage
	private float endTime = 0;//AllTimer
	public override void SkillSetting()
	{
		skillID = 2;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eCircleShotOption.Damage];
		endTime = skillData.optionArr[(int)eCircleShotOption.EndTimer];
		cooldownTime = skillData.optionArr[(int)eCircleShotOption.CoolTime];
		BulletSetting();
		delayTime = cooldownTime;
		gameObject.SetActive(false);
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			BulletLst[i].gameObject.SetActive(false);
			BulletLst[i].transform.parent = GameMng.Ins.skillMng.transform;
		}
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
				endTime += endTime * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			BulletLst[i].damage = damage;
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
	const int BulletRotationAngle = 30;
	const float Radius = 1.5f;
	private float Speed = 5;
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
				BulletLst[i].Setting(pos, moveVec);
				BulletLst[i].transform.eulerAngles = new Vector3(30, 0, -90);
				BulletLst[i].gameObject.SetActive(true);
				++count;
				continue;
			}
			if (i + 1 == BulletLst.Count)
			{
				FireBall o = Instantiate(BulletLst[0], GameMng.Ins.skillMng.transform);
				Vector3 pos = radian * bulletstartpos +
					GameMng.Ins.player.transform.position;
				Vector3 moveVec = radian * bulletstartvec;
				o.Setting(pos, moveVec);
				o.transform.eulerAngles = new Vector3(30, 0, -90);
				o.gameObject.SetActive(true);
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
