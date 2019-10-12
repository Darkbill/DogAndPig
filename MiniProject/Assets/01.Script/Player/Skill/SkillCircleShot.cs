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
	private float coolTime = 0;//Time 
	public override void SkillSetting()
	{
		skillID = 2;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eCircleShotOption.Damage];
		endTime = skillData.optionArr[(int)eCircleShotOption.EndTimer];
		coolTime = skillData.optionArr[(int)eCircleShotOption.CoolTime];
	}

	const int Angle180 = 180;
	const int BulletRotationAngle = 30;
	const float Radius = 2;
	const float settime = 4.0f;


	public FireBall Bullet;
	private int Count = 5;
	private float Speed = 5;
	private List<FireBall> BulletLst = new List<FireBall>();



	private void OnEnable()
	{
		BulletSetting();
	}
    

    private void BulletSetting()
    {
        Vector3 bulletstartpos = new Vector3(Radius, 0, 0);
        Vector3 bulletstartvec = new Vector3(0, Radius, 0);
        for(int i = 0;i<Count;++i)
        {
            GameObject o = Instantiate(Bullet.gameObject, gameObject.transform);
            Quaternion radian = Quaternion.Euler(0, 0, Angle180 * 2 / Count * i);
            o.transform.position = radian * bulletstartpos + new Vector3(0, 0, -3);
            o.GetComponent<FireBall>().BulletMovVec = radian * bulletstartvec;
            BulletLst.Add(o.GetComponent<FireBall>());
        }
    }

    private void Update()
    {
        gameObject.transform.position = GameMng.Ins.player.transform.position;
        Moving();
        
        coolTime += Time.deltaTime;
        endTime += Time.deltaTime;

        if (coolTime > 0.1 && endTime < 3.0f)
            CircleShotting();
    }

    private void Moving()
    {
        for(int i = 0;i<Count;++i)
        {
            BulletLst[i].gameObject.transform.position += BulletLst[i].BulletMovVec * Time.deltaTime * Speed;
        }
    }

    private void CircleShotting()
    {
        for(int i = 0;i<Count;++i)
        {
            Quaternion radian = Quaternion.Euler(0, 0, BulletRotationAngle);
            BulletLst[i].BulletMovVec = radian * (BulletLst[i].BulletMovVec);
        }
        coolTime = 0.0f;
    }
}
