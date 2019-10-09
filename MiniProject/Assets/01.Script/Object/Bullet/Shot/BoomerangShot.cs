using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangShot : BulletTypeSet
{
    BulletPattern bp;
    int ShotCount;
    public BoomerangShot(BulletPattern bP)
    {
        bp = bP;
    }
    public override void BulletShot()
    {
        float radianstep = Mathf.PI / 180 * 15;
        float radian = Convert.ToBoolean(ShotCount % 2) ? -ShotCount / 2 * radianstep : ShotCount / 2 * radianstep;
        for (int i = 0; i < ShotCount; ++i, radian += radianstep)
        {
            EndPos = Target + new Vector3(
            (Mathf.Cos(radian) - Mathf.Sin(radian)),
            (Mathf.Sin(radian) + Mathf.Cos(radian)),
            0);
            SelectBullet(EndPos);
        }
    }
    public override void SelectBullet(Vector3 target)
    {
        bp.SelectBullet(target);
    }
}
