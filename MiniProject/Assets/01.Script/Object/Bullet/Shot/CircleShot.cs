using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShot : BulletTypeSet
{
    BulletPattern bp;
    int ShotCount;
    public CircleShot(BulletPattern bP, int shotcount)
    {
        bp = bP;
        ShotCount = shotcount;
    }
    public override void BulletShot()
    {
        float radianstep = Mathf.PI * 2 / ShotCount;
        float radian = 0;
        for (int i = 0; i < ShotCount; ++i, radian += radianstep)
        {
            EndPos = bp.EndPos + new Vector3(
            Mathf.Cos(radian) * ShotCount,
            Mathf.Sin(radian) * ShotCount,
            0);
            SelectBullet(EndPos);
        }
    }
    public override void SelectBullet(Vector3 target)
    {
        bp.SelectBullet(target);
    }
}

