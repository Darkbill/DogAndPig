using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    //TODO : Pooling Test;

    public int Stage = 0;
    private int StageMaxMonster = 10;
    const int MapSizX = 40;
    const int MapSizY = 20;

    int MonsterMaxCounting = 0;

	public List<Monster> monsterList = new List<Monster>();
    private float worldtimer = 0;
    public Monster MilliMonsterBase;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
            NextStage();

        SponMonster(Time.deltaTime);
    }

    private void SponMonster(float deltaTime)
    {
        worldtimer += deltaTime;
        if(worldtimer >= (float)StageMaxMonster / (float)Stage)
        {
            worldtimer = 0.0f;
            for (int i = 0; i < monsterList.Count; ++i)
            {
                if(!monsterList[i].gameObject.activeSelf && MonsterMaxCounting > 0)
                {
                    --MonsterMaxCounting;
                    float setx = Rand.Range(-MapSizX, MapSizX) / 10;
                    float sety = Rand.Range(-MapSizY, MapSizY) / 10;

                    monsterList[i].transform.position = new Vector3(setx, sety, -3);
                    monsterList[i].transform.rotation = Quaternion.Euler(0, 0, Rand.Range(-180, 180));
                    monsterList[i].gameObject.SetActive(true);
                    //몬스터 초기화..
                    break;
                }
            }
        }
    }

    public void NextStage()
    {
        ++Stage;
        StageMaxCounting();
        ReloadObject();
        //몬스터 다음 레벨 초기화
    }

    private void ReloadObject()
    {
        for (int i = 0; i < monsterList.Count; ++i)
            monsterList[i].gameObject.SetActive(false);
    }

    public void StageMaxCounting()
    {
        MonsterMaxCounting = Stage * StageMaxMonster;
    }

}
