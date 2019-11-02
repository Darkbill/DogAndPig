using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationArea
{
    public Vector3 target;
    private float range = 2;

    public Vector3 TargetSetting(Vector3 pos)
    {
        Vector3 set = pos - GameMng.Ins.player.transform.position;
        set.Normalize();
        set = set * range;
        if (CheckForAreaMap(set + pos))
            return set + pos;

        set = Quaternion.Euler(0, 0, 90) * set;
        if (CheckForAreaMap(set + pos))
            return set + pos;

        set = set * -1;
        if (CheckForAreaMap(set + pos))
            return set + pos;

        set = Quaternion.Euler(0, 0, -90) * set;
        if (CheckForAreaMap(set + pos))
            return set + pos;

        return Vector3.zero;
    }

    public bool CheckForAreaMap(Vector3 pos)
    {
        if (DefineClass.MapSizX / 10 > pos.x &&
            -DefineClass.MapSizX / 10 < pos.x &&
            (DefineClass.MapSizY - 10f) / 10 > pos.y &&
            (-DefineClass.MapSizY + 10f) / 10 < pos.y)
        {
            return true;
        }
        return false;
    }
}
