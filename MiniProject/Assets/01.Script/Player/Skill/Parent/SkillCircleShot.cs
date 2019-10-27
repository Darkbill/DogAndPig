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
            BulletLst[i].damage = damage;
        }
    }
	#endregion
	const int Angle180 = 180;
	const int BulletRotationAngle = 30;
	const float Radius = 1;
	private float Speed = 5;
	public List<FireBall> BulletLst = new List<FireBall>();

	public override bool ActiveSkill()
	{
		base.ActiveSkill();
		gameObject.transform.position = GameMng.Ins.player.transform.position;
		gameObject.transform.eulerAngles = Vector3.zero;
		BulletSetting();
		return true;
	}
	private void BulletSetting()
	{
		Vector3 bulletstartpos = new Vector3(Radius, 0, 0);
		Vector3 bulletstartvec = new Vector3(0, Radius, 0);
		for (int i = 0; i < BulletLst.Count; ++i)
		{
			Quaternion radian = Quaternion.Euler(0, 0, Angle180 * 2 / BulletLst.Count * i);
			Vector3 pos = radian * bulletstartpos + new Vector3(0, 0, -3) +
                gameObject.transform.position;
			Vector3 moveVec = radian * bulletstartvec;
			BulletLst[i].Setting(pos, moveVec);
            BulletLst[i].transform.eulerAngles = new Vector3(30, 0, -90);
		}
	}

	private void FixedUpdate()
	{
		delayTime += Time.deltaTime;
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
        float SetSpeed = Speed * 50 * Time.deltaTime;
        gameObject.transform.eulerAngles += new Vector3(0, 0, SetSpeed);
        for (int i = 0; i < BulletLst.Count; ++i)
        {
            BulletLst[i].BulletMovVec = 
                BulletLst[i].transform.position - 
                GameMng.Ins.player.transform.position;
            BulletLst[i].BulletMovVec.z = 0;
            BulletLst[i].transform.eulerAngles -= new Vector3(0, 0, SetSpeed);
        }
    }
}
