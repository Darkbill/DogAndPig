using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : BulletPattern
{
    public override void SelectBullet(Vector3 target)
    {
        GameMng.Ins.bulletPool.OnBullet(target, StartPos, bulletType, bulletStyle);
    }
}
