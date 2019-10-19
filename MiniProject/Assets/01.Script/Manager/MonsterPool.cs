using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public List<Monster> monsterList = new List<Monster>();
    private int activeMonsterCount;
    public void StartStage(int stageLevel)
    {
        //스테이지 정보 불러
        List<StageDataTable> stageDataTable = JsonMng.Ins.GetStageData(stageLevel);
        activeMonsterCount = stageDataTable.Count;
        for (int i = 0; i < stageDataTable.Count; ++i)
        {
            CreateMonster(stageDataTable[i]);
        }
    }
    private void CreateMonster(StageDataTable stageData)
    {
        GameObject o = Instantiate(Resources.Load(string.Format("predator Variant_{0}", stageData.enemyIndex), typeof(GameObject))) as GameObject;
        o.transform.position = new Vector3(stageData.enemyPosX, stageData.enemyPosY,-3);
		Monster m = o.GetComponent<Monster>();
		m.monsterData.SetMonsterData(stageData.enemyLevel);
		monsterList.Add(m);
    }
    public void DeadMonster()
    {
        activeMonsterCount--;
        if(activeMonsterCount == 0)
        {
            GameMng.Ins.StageClear();
        }
    }
    //TODO : Pooling Test;

 //   public int Stage = 0;
 //   private int StageMaxMonster = 10;
 //   const int MapSizX = 40;
 //   const int MapSizY = 20;

 //   int MonsterMaxCounting = 0;

	//public List<Monster> monsterList = new List<Monster>();
 //   private float worldtimer = 0;
 //   public Monster MilliMonsterBase;

 //   private void Awake()
 //   {
        
 //   }

 //   private void Update()
 //   {
 //       if (Input.GetKeyDown("p"))
 //           NextStage();

 //       SponMonster(Time.deltaTime);
 //   }

 //   private void SponMonster(float deltaTime)
 //   {
 //       worldtimer += deltaTime;
 //       if(worldtimer >= (float)StageMaxMonster / (float)Stage)
 //       {
 //           worldtimer = 0.0f;
 //           for (int i = 0; i < monsterList.Count; ++i)
 //           {
 //               if(!monsterList[i].gameObject.activeSelf && MonsterMaxCounting > 0)
 //               {
 //                   --MonsterMaxCounting;
 //                   float setx = Rand.Range(-MapSizX, MapSizX) / 10;
 //                   float sety = Rand.Range(-MapSizY, MapSizY) / 10;

 //                   monsterList[i].transform.position = new Vector3(setx, sety, -3);
 //                   monsterList[i].transform.rotation = Quaternion.Euler(0, 0, Rand.Range(-180, 180));
 //                   monsterList[i].gameObject.SetActive(true);
 //                   //몬스터 초기화..
 //                   break;
 //               }
 //           }
 //       }
 //   }

 //   public void NextStage()
 //   {
 //       ++Stage;
 //       StageMaxCounting();
 //       ReloadObject();
 //       //몬스터 다음 레벨 초기화
 //   }

 //   private void ReloadObject()
 //   {
 //       for (int i = 0; i < monsterList.Count; ++i)
 //           monsterList[i].gameObject.SetActive(false);
 //   }

 //   public void StageMaxCounting()
 //   {
 //       MonsterMaxCounting = Stage * StageMaxMonster;
 //   }

}
