using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class SkillCircleShot : Skill
{
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

	const int Angle180 = 180;
	const int BulletRotationAngle = 30;
	const float Radius = 2;
	const float settime = 4.0f;


	public FireBall Bullet;
	private int Count = 5;
	private float Speed = 5;
	private List<FireBall> BulletLst = new List<FireBall>();


	private void BulletSetting()
	{
		Vector3 bulletstartpos = new Vector3(Radius, 0, 0);
		Vector3 bulletstartvec = new Vector3(0, Radius, 0);
		for (int i = 0; i < Count; ++i)
		{
			GameObject o = Instantiate(Bullet.gameObject, gameObject.transform);
			Quaternion radian = Quaternion.Euler(0, 0, Angle180 * 2 / Count * i);
			o.transform.position = radian * bulletstartpos + new Vector3(0, 0, -3);
			o.GetComponent<FireBall>().BulletMovVec = radian * bulletstartvec;
			BulletLst.Add(o.GetComponent<FireBall>());
		}
	}

	public override void ActiveSkill()
	{
		base.ActiveSkill();
        gameObject.transform.position = Vector3.zero;
        ReSizBullet();
        endTime = 0.0f;
	}

    private void ReSizBullet()
    {
        Vector3 bulletstartpos = new Vector3(Radius, 0, 0);
        Vector3 bulletstartvec = new Vector3(0, Radius, 0);

        int count = 0;
        for (int i = 0; i < BulletLst.Count; ++i)
        {
            if (count < 5 && i + 1 == BulletLst.Count)
            {
                CreateBullet(count, bulletstartpos, bulletstartvec);
                ++count;
            }
            if (count < 5 && !BulletLst[i].gameObject.activeSelf)
            {
                BulletLst[i].transform.position =
                    Quaternion.Euler(0, 0, Angle180 * 2 / Count * count) *
                    bulletstartpos +
                    new Vector3(0, 0, -3);
                BulletLst[i].BulletMovVec =
                    Quaternion.Euler(0, 0, Angle180 * 2 / Count * count) *
                    bulletstartvec;
                BulletLst[i].gameObject.SetActive(true);
                ++count;
                BulletLst[i].SetTimer = 0.0f;
            }
            
        }
    }

    private void CreateBullet(int radnum, Vector3 startpos, Vector3 startvec)
    {
        GameObject o = Instantiate(Bullet.gameObject, gameObject.transform);
        Quaternion radian = Quaternion.Euler(0, 0, Angle180 * 2 / Count * radnum);
        o.transform.position = radian * startpos + new Vector3(0, 0, -3);
        o.GetComponent<FireBall>().BulletMovVec = radian * startvec;
        BulletLst.Add(o.GetComponent<FireBall>());
    }

    private void Update()
    {
		delayTime += Time.deltaTime;
		gameObject.transform.position = GameMng.Ins.player.transform.position;
        Moving();
		endTime += endTime;
        if (endTime > 0.1)
            CircleShotting();
    }

    private void Moving()
    {
        for(int i = 0;i<BulletLst.Count;++i)
        {
            BulletLst[i].gameObject.transform.position += BulletLst[i].BulletMovVec * Time.deltaTime * Speed;
        }
    }

    private void CircleShotting()
    {
        for(int i = 0;i<BulletLst.Count;++i)
        {
            if (BulletLst[i].SetTimer <= 2.0f)
            {
                Quaternion radian = Quaternion.Euler(0, 0, BulletRotationAngle);
                BulletLst[i].BulletMovVec = radian * (BulletLst[i].BulletMovVec);
            }
        }
		endTime = 0.0f;
    }
}
