using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
	public List<Monster> monsterList = new List<Monster>();
	public Dictionary<int, List<StageDataTable>> worldData = new Dictionary<int, List<StageDataTable>>();
	public List<GameObject> monsterEffectList = new List<GameObject>();
	public int bossIndex = -1;
	private int spawnMonsterCount = 0;
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
				string.Format("MonsterCreateEffect"),
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
	public void WorldStart(int worldLevel)
	{
		//월드 시작시 해당 월드 모든 몬스터 생성해서 저장
		monsterList.Clear();
		worldData = JsonMng.Ins.GetWorldData(worldLevel);
		var i = worldData.GetEnumerator();
		while(i.MoveNext())
		{
			var stageList = i.Current.Value;
			for(int j = 0; j < stageList.Count; ++j)
			{
				CreateMonster(stageList[j]);
			}
		}
		StartStage(1);
	}
	public void StartStage(int stageLevel)
	{
		List<StageDataTable> stageDataTable = worldData[stageLevel];
		activeMonsterCount = stageDataTable.Count;
		for (int i = 0; i < stageDataTable.Count; ++i)
		{
			monsterList[spawnMonsterCount].gameObject.SetActive(true);
			if (stageDataTable[i].boss == 1) SetBossInfo(spawnMonsterCount);
			else GameMng.Ins.SetHPMonster(monsterList[spawnMonsterCount]);
			SelectEffect(monsterList[spawnMonsterCount].transform.position);
			
			spawnMonsterCount++;
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
		m.gameObject.SetActive(false);
		monsterList.Add(m);
	}
    private float AnglePlayerSetting(Vector3 pos)
    {
		//4분활 위치별 플레이어 바라보도록 Angle변경
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
	public void DeadMonster()
	{
		activeMonsterCount--;
		if (activeMonsterCount == 0)
		{
			GameMng.Ins.StageClear();
		}
	}
	public float GetBossFill()
	{
		return monsterList[bossIndex].monsterData.healthPoint / JsonMng.Ins.monsterDataTable[monsterList[bossIndex].MonsterID].healthPoint;
	}

}