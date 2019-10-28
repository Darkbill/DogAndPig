using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    Dictionary<eAttackType, List<HitBase>> effectlist = 
        new Dictionary<eAttackType, List<HitBase>>();

    const int PoolSiz = 20;

    private void Awake()
    {
        //TODO : Asset을 불러와서 풀링, 각 속성마다 20개씩 총 100개
        CreateEffect("TestHitEffect", eAttackType.Physics);
        CreateEffect("TestHitEffect", eAttackType.Fire);
        CreateEffect("TestHitEffect", eAttackType.Water);
        CreateEffect("TestHitEffect", eAttackType.Lightning);
        CreateEffect("TestHitEffect", eAttackType.Wind);
    }

    void CreateEffect(string effectname, eAttackType type)
    {
        List<HitBase> lst = new List<HitBase>();
        effectlist.Add(type, lst);
        for (int i = 0;i<PoolSiz;++i)
        {
            GameObject o = Instantiate(
                Resources.Load(string.Format("Effect/{0}", effectname),
                typeof(GameObject)))
                as GameObject;
            o.transform.position = gameObject.transform.position;
            o.transform.parent = gameObject.transform;
            o.SetActive(false);
            HitBase eff = o.GetComponent<HitBase>();
            effectlist[type].Add(eff);
        }
    }


    public void RunHitAnimation(eAttackType type, Vector3 target, Vector3 pos)
    {
        for(int i = 0;i<effectlist[type].Count;++i)
        {
            if(!effectlist[type][i].gameObject.activeSelf)
            {
                Vector3 right = target - pos;
                right.Normalize();
                effectlist[type][i].transform.position = pos + right;
                effectlist[type][i].gameObject.SetActive(true);
                break;
            }
        }
		//TODO : 추가생성
    }
}
