using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern
{
    //TODO : player에서도 쓰일거같아서 따로 나눔

    Vector3 startPos;
    Vector3 endPos;

    eBulletType bulletType = eBulletType.None;

    public BulletPattern(eBulletType type)
    {
        bulletType = type;
    }

    public void SettingPos(Vector3 endpos, Vector3 startpos)
    {
        startPos = startpos;
        endPos = endpos;
    }

    private void SelectBulletType(Vector3 endpos, Vector3 startpos)
    {
        switch(bulletType)
        {
            case eBulletType.None:
                break;
            case eBulletType.Player:
                GameMng.Ins.bulletPool.OnBullet(endpos, startpos);
                break;
            case eBulletType.Monster:
                GameMng.Ins.bulletMonster.OnBullet(endpos, startpos);
                break;
            case eBulletType.hostility:
                break;
        }
    }

    public void OneShot()//단발
    {
        SelectBulletType(endPos, startPos);
    }
    public void BunchShot()//다발
    {
        Vector3 target = endPos + new Vector3(
            UnityEngine.Random.Range(-0.3f, 0.3f),
            UnityEngine.Random.Range(-0.3f, 0.3f),
            0);
        SelectBulletType(target, startPos);
    }
    public void CircleShot(int shotcount)//매개변수: 탄환 개수 원형탄
    {
        float radianstep = Mathf.PI * 2 / shotcount;
        float radian = 0;
        for (int i = 0; i < shotcount; ++i, radian += radianstep)
        {
            Vector3 target = endPos + new Vector3(
            Mathf.Cos(radian) * shotcount,
            Mathf.Sin(radian) * shotcount,
            0);
            SelectBulletType(target, startPos);
        }
    }
    public void ShotGun(int shotcount)//샷건
    {
        float radianstep = Mathf.PI / 180 * 15;
        float radian = Convert.ToBoolean(shotcount % 2) ? -shotcount / 2 * radianstep : shotcount / 2 * radianstep;
        for (int i = 0; i < shotcount; ++i, radian += radianstep)
        {
            Vector3 target = GameMng.Ins.player.transform.position + new Vector3(
            (Mathf.Cos(radian) - Mathf.Sin(radian)),
            (Mathf.Sin(radian) + Mathf.Cos(radian)),
            0);
            SelectBulletType(target, startPos);
        }
    }
}
