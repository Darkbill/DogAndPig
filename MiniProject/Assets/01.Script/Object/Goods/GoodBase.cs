using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GlobalDefine;

public class GoodBase : MonoBehaviour
{
    public int goodid { get; private set; }
    public int amount = 0;
    //TODO : id 1 = coin 2 = dia

    public virtual void BaseSetting(int id)
    {
        goodid = id;
    }

    public virtual void Running(Vector3 startpos, float range, int amnt)
    {
        gameObject.transform.position = startpos;

        float randnum = Rand.Range(-180, 180);
        Vector3 endpos = new Vector3(Mathf.Cos(randnum * Mathf.Deg2Rad), Mathf.Sin(randnum * Mathf.Deg2Rad)) * range;

        gameObject.transform.DOMove(endpos + startpos, 0.5f, false);
        amount = amnt;
    }

    public virtual void ClearRunning()
    {
        gameObject.transform.DOMove(GameMng.Ins.player.transform.position, 0.5f, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            OnTriggetEntetObject();
    }

    public virtual void OnTriggetEntetObject()
    {
        gameObject.SetActive(false);
    }
}
