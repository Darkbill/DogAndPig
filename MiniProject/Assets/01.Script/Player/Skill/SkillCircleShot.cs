﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCircleShot : MonoBehaviour
{
    public FireBall Bullet;
    private int Count = 5;
    private float Speed = 5;

    const int Angle180 = 180;
    const int BulletRotationAngle = 30;
    const float Radius = 2;
    const float settime = 4.0f;

    private float timer = 0.0f;
    private float AllTimer = 0.0f;

    private List<FireBall> BulletLst = new List<FireBall>();

    private Vector3 moveDirection;

    private float Option01 = 0;//Damage
    private float Option02 = 0;//AllTimer
    private float Option03 = 0;//Time


    private void Awake()
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
        
        timer += Time.deltaTime;
        AllTimer += Time.deltaTime;

        if (timer > 0.1 && AllTimer < 3.0f)
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
        timer = 0.0f;
    }
}