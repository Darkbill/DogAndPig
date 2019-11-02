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
		float range = dirbetween.magnitude;
        if(range <= attrange)
        {
            float settingdegree = Vector3.Angle(dirVec.normalized, dirbetween.normalized);
            Debug.Log(settingdegree);
            if (settingdegree <= degree)
            {
                return true;
            }
        }
        return false;
    }
    public Vector3 GetFindShortStreet(List<Monster> monsterdata, Vector3 pos, float attrange, float rightx)
    {
        float shortestrange = attrange;
        int idxnum = -1;
        for(int i = 0;i<monsterdata.Count;++i)
        {
            if (monsterdata[i] == null) continue;
            float monstersiz = monsterdata[i].monsterData.size;
            float playersiz = 0.3f;
            float range = (monsterdata[i].transform.position + new Vector3(0, monstersiz, 0) -
                pos + new Vector3(0, playersiz, 0)).magnitude;
            if ((rightx < 0 && monsterdata[i].transform.position.x - pos.x > 0) ||
                (rightx > 0 && monsterdata[i].transform.position.x - pos.x < 0))
                continue;
            if (range <= shortestrange)
            {
                shortestrange = range;
                idxnum = i;
            }
        }
        if(idxnum < 0)
            return new Vector3(rightx, 0, 0);
        return (monsterdata[idxnum].transform.position - pos);
    }
}
