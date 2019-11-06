using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    Dictionary<eAttackType, List<HitBase>> effectlist = 
        new Dictionary<eAttackType, List<HitBase>>();

    List<Targetting> HitTargetEff = new List<Targetting>();

    const int PoolSiz = 20;
    const int HitTargetSiz = 100;

    private void Awake()
    {
        CreateEffect("PhysicsHitEff", eAttackType.Physics);
        CreateEffect("FireHitEff", eAttackType.Fire);
        CreateEffect("IceHitEff", eAttackType.Water);
        CreateEffect("LightningHitEff", eAttackType.Lightning);
        CreateEffect("WindHitEff", eAttackType.Wind);

        CreateHitTargetEff("Targetting");
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

    void CreateHitTargetEff(string effectname)
    {
        for(int i = 0;i<HitTargetSiz;++i)
        {
            GameObject o = Instantiate(
                Resources.Load(string.Format("Effect/{0}", effectname),
                typeof(GameObject)))
                as GameObject;
            o.transform.position = gameObject.transform.position;
            o.transform.parent = gameObject.transform;
            o.SetActive(false);
            Targetting eff = o.GetComponent<Targetting>();
            eff.EndPlaying();
            HitTargetEff.Add(eff);
        }
    }


    public void RunHitAnimation(eAttackType type, Vector3 target, Vector3 pos, float siz)
    {
        Vector3 right = target - pos;
        for (int i = 0;i<effectlist[type].Count;++i)
        {
            if(!effectlist[type][i].gameObject.activeSelf)
            {
                right.Normalize();
                effectlist[type][i].transform.position = pos + right * 0.3f + new Vector3(0, siz);
                effectlist[type][i].gameObject.SetActive(true);
                return;
            }
        }
        //TODO : 추가생성
        HitBase o = Instantiate(effectlist[type][0]);
        o.transform.position = pos + right * 0.5f;
        o.transform.parent = gameObject.transform;
        o.gameObject.SetActive(true);
        effectlist[type].Add(o);

    }

    public void GetHitTargetEff(Vector3 pos,int skillID)
    {
        for(int i = 0;i< HitTargetEff.Count;++i)
        {
            if (!HitTargetEff[i].gameObject.activeSelf)
            {
                HitTargetEff[i].transform.position = pos;
				HitTargetEff[i].skillID = skillID;
				HitTargetEff[i].gameObject.SetActive(true);
                HitTargetEff[i].Playing();
                return;
            }
        }
    }
}
