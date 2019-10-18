using System.Collections;
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
        if (dirVec == Vector3.zero)
            return false;
        float range = dirbetween.magnitude;
        if(range < attrange)
        {
            Vector3 target = dirbetween.normalized;
            dirVec += new Vector3(0, 0, target.z);

            if (Mathf.Abs(Vector3.Angle(dirVec, target)) <= degree)
            {
                return true;
            }
        }
        return false;
    }
}
