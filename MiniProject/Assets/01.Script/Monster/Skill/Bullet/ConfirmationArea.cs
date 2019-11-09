using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationArea
{
    public Vector3 target;
    private float range = 2;
    //TODO : 해당 메소드들은 몬스터한테만 사용가능. 플레이어 사용x
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

    public Vector3 RangeRandomResult(Vector3 pos, float range)
    {
        float randnum = Rand.Range(-180, 180);

        Vector3 set = Quaternion.Euler(0, 0, randnum) * Vector3.right * range;

        if(CheckForAreaMap(pos + (set)))
            return pos + set;

        for(int i = 0;i<3;++i)
        {
            set = Quaternion.Euler(0, 0, 90) * set;
            if (CheckForAreaMap(pos + set))
                return pos + set;
        }

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
