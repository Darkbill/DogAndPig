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
    private Vector3 startPos;
    private Vector3 endPos;

    private bool setground = false;
    private bool setclear = false;

    public virtual void BaseSetting(int id)
    {
        goodid = id;
    }

    public virtual void Running(Vector3 startpos, float range, int amnt)
    {
        setground = false;
        setclear = false;
        gameObject.transform.position = startpos;
        startPos = startpos;

        float randnum = Rand.Range(-180, 180);
        Vector3 endpos = new Vector3(Mathf.Cos(randnum * Mathf.Deg2Rad), Mathf.Sin(randnum * Mathf.Deg2Rad)) * range;

        gameObject.transform.DOLocalJump(endpos + startpos, 0.5f, 1, 0.5f, false).OnComplete(()=>
        {
            setground = true;
        });
        amount = amnt;
    }

    public virtual void ClearRunning()
    {
        setclear = true;
        
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

    private void Update()
    {
        endPos = GameMng.Ins.player.transform.position;
        if (setground && setclear)
        {
            gameObject.transform.DOMove(gameObject.transform.position + new Vector3(0, 0.5f), 0.5f, false).OnComplete(() =>
            {
				if (gameObject.activeSelf)
				{
					StartCoroutine(MovSetRuttine());
				}
            });
            setground = false;
            return;
        }
        else if((startPos - GameMng.Ins.player.transform.position).magnitude < 2 && setground && !setclear)
        {
			if (gameObject.activeSelf)
			{
				StartCoroutine(MovSetRuttine());
			}
        }
    }

    private IEnumerator MovSetRuttine()
    {
        setground = false;
        while (true)
        {
            float range = (gameObject.transform.position - endPos).magnitude;

            if (range < 0.5f)
                break;
            Vector3 movevec = (endPos - gameObject.transform.position).normalized;
            gameObject.transform.position += movevec * Time.deltaTime * 5;
            yield return null;
        }
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
