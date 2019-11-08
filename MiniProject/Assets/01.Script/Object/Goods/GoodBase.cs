using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GlobalDefine;

public class GoodBase : MonoBehaviour
{
    public int goodid { get; private set; }
    //TODO : id 1 = coin 2 = dia

    public virtual void BaseSetting(int id)
    {
        goodid = id;
    }

    public virtual void Running(Vector3 startpos, float range)
    {
        gameObject.transform.position = startpos;

        float randnum = Rand.Range(-180, 180);
        Vector3 endpos = new Vector3(Mathf.Cos(randnum * Mathf.Deg2Rad), Mathf.Sin(randnum * Mathf.Deg2Rad)) * range;

        gameObject.transform.DOMove(endpos + startpos, 0.5f, false);
    }

    public virtual void ClearRunning()
    {

    }

    public virtual void Crash(Player player)
    {
        gameObject.SetActive(false);
        //TODO : 돈이 올라가는 정도 표시 - 호출
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
