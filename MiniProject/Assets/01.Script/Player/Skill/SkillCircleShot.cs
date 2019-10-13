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
	}
	#endregion
	const int Angle180 = 180;
	const int BulletRotationAngle = 30;
	const float Radius = 1;
	private float Speed = 5;
	public List<FireBall> BulletLst = new List<FireBall>();

	public override void ActiveSkill()
	{
		base.ActiveSkill();
		gameObject.transform.position = Vector3.zero;
		gameObject.transform.eulerAngles = Vector3.zero;
		BulletSetting();
	}
	private void BulletSetting()
	{
		Vector3 bulletstartpos = new Vector3(Radius, 0, 0);
		Vector3 bulletstartvec = new Vector3(0, Radius, 0);
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			Quaternion radian = Quaternion.Euler(0, 0, Angle180 * 2 / BulletLst.Count * i);
			BulletLst[i].transform.position = radian * bulletstartpos + new Vector3(0, 0, -3);
			BulletLst[i].BulletMovVec = radian * bulletstartvec;
			BulletLst[i].gameObject.SetActive(true);
		}
	}

	private void FixedUpdate()
	{
		delayTime += Time.deltaTime;
		Debug.Log(delayTime);
		if (delayTime >= endTime)
		{
			MovingDirection();
		}
		else
		{
			gameObject.transform.position = GameMng.Ins.player.transform.position;
			MovingCircle();
		}
	}

    private void MovingDirection()
    {
        for(int i = 0;i<BulletLst.Count;++i)
        {
            BulletLst[i].gameObject.transform.position += BulletLst[i].BulletMovVec * Time.deltaTime * Speed;
        }
    }

    private void MovingCircle()
    {
		gameObject.transform.eulerAngles += new Vector3(0, 0, Speed * 50) * Time.deltaTime;
	}
}
