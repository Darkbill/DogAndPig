using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThounderSystem : MonoBehaviour
{
    public List<Thounder> thounderlist = new List<Thounder>();
    private int count;
    private Vector3 startpos = new Vector3();
    private bool skillOn = false;
    private int range;
    private float maxCount;


    public void Setting(float _dmg, int _range, float _maxcount)
    {
        foreach (Thounder o in thounderlist)
            o.Setting(_dmg);
        range = _range;
        maxCount = _maxcount;
        gameObject.transform.parent = GameMng.Ins.skillMng.transform;
    }

    public void SystemSetting(Vector3 _startpos)
    {
        startpos = _startpos;
        gameObject.SetActive(true);
        count = 0;
        skillOn = true;
    }

    private void Update()
    {
        if (skillOn && count <= maxCount)
        {
            SkillCall();
        }
    }

    private void SkillCall()
    {
        thounderlist[0].SettingSystem(startpos);
        for (int i = 1; i < thounderlist.Count; ++i)
        {
            float randx = (float)Rand.Range(-range, range) / 10.0f;
            float randy = (float)Rand.Range(-range, range) / 10.0f;
            if (count + 1 <= i)
                break;
            thounderlist[i].transform.position =
                thounderlist[0].transform.position +
                new Vector3(randx, randy);
            thounderlist[i].SettingSystem(thounderlist[0].transform.position + new Vector3(randx, randy));
        }
        if(count + 1 >= thounderlist.Count)
            CreateThounder();
        ++count;
        skillOn = false;
        StartCoroutine(SkillHitCheck());
    }

    private void CreateThounder()
    {
        float randx = (float)Rand.Range(-range, range) / 10.0f;
        float randy = (float)Rand.Range(-range, range) / 10.0f;

        Thounder o = Instantiate(thounderlist[0], gameObject.transform);
        o.Setting(thounderlist[0].damage);
        o.SettingSystem(thounderlist[0].transform.position + new Vector3(randx, randy));
        thounderlist.Add(o);
    }

    private IEnumerator SkillHitCheck()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < thounderlist.Count; ++i)
        {
            if (thounderlist[i].hit)
            {
                thounderlist[i].hit = false;
                skillOn = true;
            }
        }
        if (!skillOn) gameObject.SetActive(false);
    }
}
