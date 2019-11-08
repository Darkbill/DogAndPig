using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodMng : MonoBehaviour
{
    public List<GoodBase> goodlist = new List<GoodBase>();
    private const int goldpoolCnt = 80;
    private const int diamonspoolCnt = 20;
    //TODO : Gold 80 / Dia 20 pooling

    public Gold goldbase;
    public Diamond diabase;

    private void Awake()
    {
        for(int i = 0;i< goldpoolCnt; ++i)
        {
            Gold o = Instantiate(goldbase, gameObject.transform);
            o.BaseSetting(1);
            o.gameObject.SetActive(false);
            goodlist.Add(o);
        }
        for(int i = 0;i<diamonspoolCnt;++i)
        {
            Diamond o = Instantiate(diabase, gameObject.transform);
            o.BaseSetting(2);
            o.gameObject.SetActive(false);
            goodlist.Add(o);
        }
    }

    public void RunningSelect(int id, int count, Vector3 startpos)
    {
        float cnt = count;

        for (int i = 0; i < goodlist.Count; ++i)
        {
            if (cnt <= 0) break;
            if (goodlist[i].goodid == id && !goodlist[i].gameObject.activeSelf)
            {
                goodlist[i].gameObject.SetActive(true);
                goodlist[i].Running(startpos, 1.0f);
                --cnt;
            }
        }
    }

    public void AllRunningSelect()
    {
        foreach(GoodBase o in goodlist)
            if(o.gameObject.activeSelf)
                o.ClearRunning();
    }
}
