using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangShot : BulletTypeSet
{
    BulletPattern bp;
    int ShotCount;
    public BoomerangShot(BulletPattern bP, int shotcount)
    {
        bp = bP;
        ShotCount = shotcount;
        bp.bulletStyle = eBulletStyle.BOOMERANG;
    }
    public override void BulletShot()
    {
        float degreestep = 15;
        float degree = Convert.ToBoolean(ShotCount % 2) ? -ShotCount / 2 * degreestep : ShotCount / 2 * degreestep;
        for (int i = 0; i < ShotCount; ++i, degree += degreestep)
        {
            var quater = Quaternion.Euler(0, 0, degree);
            EndPos = quater * bp.Target;
            SelectBullet(EndPos);
        }
    }
    public override void SelectBullet(Vector3 target)
    {
        bp.SelectBullet(target);
    }
}
