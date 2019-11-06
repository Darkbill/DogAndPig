using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
	public List<Monster> monsterList = new List<Monster>();
	public List<GameObject> monsterEffectList = new List<GameObject>();
	public int bossIndex = -1;
	private int activeMonsterCount;

	private const int poolSiz = 20;

	Dictionary<eBuffType, List<BuffBase>> effectlist = new Dictionary<eBuffType, List<BuffBase>>();

	private void Awake()
	{
		MonsterCreateEffectSet();
		MonsterBuffEffect("BufElectricShock", eBuffType.Stun);
		MonsterBuffEffect("BufFreezen", eBuffType.MoveSlow);
	}

	private void MonsterCreateEffectSet()
	{
		for (int i = 0; i < poolSiz; ++i)
		{
			GameObject eff = Instantiate(Resources.Load(
				string.Format("MonsterCreateTestEffect"),
				typeof(GameObject))) as GameObject;
			eff.transform.position = new Vector3();
			eff.SetActive(false);
			monsterEffectList.Add(eff);
		}
	}

	private void MonsterBuffEffect(string effectname, eBuffType type)
	{
		List<BuffBase> lst = new List<BuffBase>();
		effectlist.Add(type, lst);
		for (int i = 0; i < poolSiz; ++i)
		{
			GameObject o = Instantiate(
				Resources.Load(string.Format("Effect/{0}", effectname),
				typeof(GameObject)))
				as GameObject;
			o.transform.position = gameObject.transform.position;
			o.transform.parent = gameObject.transform;
			o.SetActive(false);
			BuffBase eff = o.GetComponent<BuffBase>();
			effectlist[type].Add(eff);
		}
	}

	public void ResetMonster()
	{
		for(int i = 0; i < monsterList.Count; ++i)
		{
			Destroy(monsterList[i].gameObject);
		}
	}
	public void SelectEffect(GameObject monsterobj, ConditionData data)
	{
		for (int i = 0; i < effectlist[data.buffType].Count; ++i)
		{
			if (!effectlist[data.buffType][i].gameObject.activeSelf)
			{
				effectlist[data.buffType][i].gameObject.SetActive(true);
				effectlist[data.buffType][i].Setting(data, monsterobj);
				return;
			}
			if (effectlist[data.buffType][i].settingObj == monsterobj)
			{
				effectlist[data.buffType][i].Setting(data, monsterobj);
				return;
			}
		}
	}

	public void StartStage(int stageLevel)
	{
		monsterList.Clear();
		List<StageDataTable> stageDataTable = JsonMng.Ins.GetStageData(stageLevel);
		activeMonsterCount = stageDataTable.Count;
		for (int i = 0; i < stageDataTable.Count; ++i)
		{
			CreateMonster(stageDataTable[i]);
			if (stageDataTable[i].boss == 1) SetBossInfo(i);
		}
	}
	private void SetBossInfo(int _bossIndex)
	{
		bossIndex = _bossIndex;
		UIMngInGame.Ins.SetBossInfo();
	}
	private void CreateMonster(StageDataTable stageData)
	{
		GameObject o = Instantiate(Resources.Load(string.Format("Monster/predator Variant_{0}", stageData.enemyIndex), typeof(GameObject))) as GameObject;
		o.transform.position = new Vector3(stageData.enemyPosX, stageData.enemyPosY, 0);
		Monster m = o.GetComponent<Monster>();
        m.Angle = AnglePlayerSetting(m.transform.position);
		m.monsterData.SetMonsterData(stageData.enemyLevel);
		monsterList.Add(m);
		SelectEffect(m.transform.position);
	}
    private float AnglePlayerSetting(Vector3 pos)
    {
        Vector3 rightvec = GameMng.Ins.player.transform.position - pos;
        float angle = Mathf.Atan2(rightvec.y, rightvec.x) * Mathf.Rad2Deg;
        if (angle < -90)
            return Rand.Range(-180, -90);
        else if (angle < 0)
            return Rand.Range(-90, 0);
        else if (angle < 90)
            return Rand.Range(0, 90);
        else
            return Rand.Range(90, 180);
        //return Mathf.Atan2(rightvec.y, rightvec.x) * Mathf.Rad2Deg;
    }

    private void SelectEffect(Vector3 pos)
	{
		for (int i = 0; i < monsterEffectList.Count; ++i)
		{
			if (!monsterEffectList[i].activeSelf)
			{
				monsterEffectList[i].transform.position = pos;
				monsterEffectList[i].SetActive(true);
				StartCoroutine(IEWaitEffect(i));
				return;
			}
		}
		GameObject eff = Instantiate(Resources.Load(
				string.Format("MonsterCreateTestEffect"),
				typeof(GameObject))) as GameObject;
		eff.transform.position = pos;
		eff.SetActive(true);
		monsterEffectList.Add(eff);

	}
	private IEnumerator IEWaitEffect(int num)
	{
		yield return new WaitForSeconds(2.0f);
		monsterEffectList[num].SetActive(false);
	}
	public void DeadMonster(GameObject obj)
	{
		activeMonsterCount--;
		if (activeMonsterCount == 0)
		{
			GameMng.Ins.StageClear();
		}
		//for (eBuffType i = eBuffType.MoveFast; i < eBuffType.Max; ++i)
		//{
		//    for (int j = 0; j < effectlist[i].Count; ++j)
		//    {
		//        if (effectlist[i][j].settingObj == obj)
		//            effectlist[i][j].gameObject.SetActive(false);
		//    }
		//}
	}
	public float GetBossFill()
	{
		return monsterList[bossIndex].monsterData.healthPoint / JsonMng.Ins.monsterDataTable[monsterList[bossIndex].MonsterID].healthPoint;
	}

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