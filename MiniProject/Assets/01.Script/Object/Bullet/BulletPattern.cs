using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletPattern
{
    public Vector3 StartPos;
    public Vector3 EndPos;

    public Vector3 Target;

    protected eBulletType bulletType = eBulletType.None;

    public abstract void SelectBullet(Vector3 target);
    public void BulletShot() { }

    public void SettingPos(Vector3 endpos, Vector3 startpos, eBulletType bulletype)
    {
        StartPos = startpos;
        EndPos = endpos;
        bulletType = bulletype;

        Target = EndPos;
    }
}