using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCircleShot : MonoBehaviour
{
    public GameObject Bullet;
    private int Count = 0;
    private float Speed = 5;

    const int Angle180 = 180;
    const int BulletRotationAngle = 30;
    const float Radius = 2;
    const float settime = 4.0f;

    private float timer = 0.0f;
    
    private List<SkillBullet> BulletLst = new List<SkillBullet>();

    // Start is called before the first frame update

    
    public void Setting(int bulletcnt, float bulletSpeed)
    {
        Count = bulletcnt;
        Speed = bulletSpeed;
        BulletSetting();
    }

    private void BulletSetting()
    {
        Vector3 bulletstartpos = new Vector3(Radius, 0, 0);
        Vector3 bulletstartvec = new Vector3(0, Radius, 0);
        for(int i = 0;i<Count;++i)
        {
            SkillBullet bullet = new SkillBullet();
            bullet.BulletObject = Instantiate<GameObject>(Bullet);
            Quaternion radian = Quaternion.Euler(0, 0, Angle180 * 2 / Count * i);
            bullet.BulletObject.transform.position = radian * bulletstartpos + new Vector3(0, 0, -3);
            bullet.BulletMovVec = radian * bulletstartvec;
            bullet.AwakeSet();
            BulletLst.Add(bullet);
        }
    }

    public void ObjectUpdate(Vector3 hostpos)
    {
        Moving(hostpos);
        timer += Time.deltaTime;
        if (timer > 0.1)
            CircleShotting();
    }

    private void Moving(Vector3 hostpos)
    {
        for(int i = 0;i<Count;++i)
        {
            BulletLst[i].UpdateTarget(BulletLst[i].BulletMovVec + hostpos);
            BulletLst[i].BulletUpdate();
        }
    }

    private void CircleShotting()
    {
        for(int i = 0;i<Count;++i)
        {
            Quaternion radian = Quaternion.Euler(0, 0, BulletRotationAngle);
            BulletLst[i].BulletMovVec = radian * (BulletLst[i].BulletMovVec);
            BulletLst[i].BulletMovVec.z = -3;
        }
        timer = 0.0f;
    }
}
