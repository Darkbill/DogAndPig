using UnityEngine;

public class ObjectSetAttack
{
    //dirVec : 해당 오브젝트의 방향 벡터
    //dirbetween : 두 오브젝트의 벡터
    //attrange : 범위/반지름
    //degree : 범위 각도
    public bool BaseAttack(Vector3 dirVec, Vector3 dirbetween, float attrange, float degree)
    {
		dirbetween.z = 0;
		float range = dirbetween.magnitude;
        if(range <= attrange)
        {
            if (Vector3.Angle(dirVec.normalized, dirbetween.normalized) <= degree)
            {
                return true;
            }
        }
        return false;
    }
}
