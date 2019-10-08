using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunchShot : BulletTypeSet
{
    BulletPattern bp;
    public BunchShot(BulletPattern bP)
    {
        bp = bP;
    }
    public override void BulletShot()
    {
        EndPos = Target + new Vector3(
            UnityEngine.Random.Range(-0.3f, 0.3f),
            UnityEngine.Random.Range(-0.3f, 0.3f),
            0);
        SelectBullet(EndPos);
    }
    public override void SelectBullet(Vector3 target)
    {
        bp.SelectBullet(target);
    }
}

