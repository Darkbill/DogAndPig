using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodMng : MonoBehaviour
{
    public List<GoodBase> goodlist = new List<GoodBase>();
    private const int goldpoolCnt = 80;
    private const int diamonspoolCnt = 20;

    private const int experienceCnt = 100;
    private const int rangeMaxCnt = 7;


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
        int maxcnt = rangeMaxCnt;
        int rangecnt = 0;
        for (int i = 0; i < goodlist.Count; ++i)
        {
            if (cnt <= 0) break;
            if(maxcnt <= 0)
            {
                ++rangecnt;
                maxcnt = rangeMaxCnt;
            }
            if (goodlist[i].goodid == id && !goodlist[i].gameObject.activeSelf)
            {
                goodlist[i].gameObject.SetActive(true);
                goodlist[i].Running(startpos, 0.5f + 0.5f * rangecnt, 3);
                --cnt;
                maxcnt--;
            }
        }
    }

    public void AllRunningSelect()
    {
        foreach(GoodBase o in goodlist)
            if(o.gameObject.activeSelf)
                o.ClearRunning();
    }

    public bool GoodsExistence()
    {
        foreach (GoodBase o in goodlist)
            if (o.gameObject.activeSelf)
                return false;
        return true;
    }
}
