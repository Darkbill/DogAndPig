using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaShot : BulletTypeSet
{
    BulletPattern bp;
    private float degree = 105;

    public ParabolaShot(BulletPattern bP)
    {
        bp = bP;
        if (bp.leritype > 0)
            bp.bulletStyle = eBulletStyle.PARABOLALEFT;
        else
            bp.bulletStyle = eBulletStyle.PARABOLARIGHT;
    }
    public override void BulletShot()
    {
        var quater = Quaternion.Euler(0, 0, degree * bp.leritype);
        EndPos = quater * bp.Target;
        SelectBullet(EndPos);
    }
    public override void SelectBullet(Vector3 target)
    {
        bp.SelectBullet(target);
    }
}
