﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetAttack
{
    //dirVec : 해당 오브젝트의 방향 벡터
    //dirbetween : 두 오브젝트의 벡터
    //attrange : 범위/반지름
    //degree : 범위 각도
    public bool BaseAttack(Vector3 dirVec, Vector3 dirbetween, float attrange, float degree)
    {
        if (dirVec == new Vector3(0, 0, 0))
            return false;
        float range = Mathf.Sqrt(Mathf.Pow(dirbetween.x, 2) + Mathf.Pow(dirbetween.y, 2));
        if(range < attrange)
        {
            Vector3 target = dirbetween.normalized;

            if (Vector3.Angle(dirVec, target) < degree)
            {
                return true;
            }
        }
        return false;
    }
}
