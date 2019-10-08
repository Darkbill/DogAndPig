using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShot : BulletTypeSet
{
    BulletPattern bp;
    public OneShot(BulletPattern bP)
    {
        bp = bP;
    }
    public override void BulletShot()
    {
        SelectBullet(EndPos);
    }
    public override void SelectBullet(Vector3 target)
    {
        bp.SelectBullet(target);
    }
}

