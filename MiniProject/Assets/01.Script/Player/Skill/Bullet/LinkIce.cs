using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkIce : BulletPlayerSkill
{
	public float damage;
	eAttackType Attacktype = eAttackType.Water;
	eBuffType bufftype = eBuffType.MoveSlow;
	private int Id;
    private float per;
	private float endTime;
	private float changeValue;
	private float speed;
	private int maxHitCount;
	private Vector3 nextPos;

    private List<Monster> monsterpool;
	private List<int> hitMonsterPool = new List<int>();
	public void Setting(int id, float p, float _damage,float _endTime,float _changeValue,float iceSpeed,int mC)
    {
        Id = id;
        per = p;
		damage = _damage;
		endTime = _endTime;
		changeValue = _changeValue;
		speed = iceSpeed;
		maxHitCount = mC;
		gameObject.SetActive(false);
	}
	public void SystemSetting()
	{
		gameObject.SetActive(true);
		hitMonsterPool.Clear();
		gameObject.transform.position = GameMng.Ins.player.transform.position +
								new Vector3(0, GameMng.Ins.player.calStat.size);
		NextPosSet();
	}
    private void NextPosSet()
    {
		try
		{
			monsterpool = GameMng.Ins.monsterPool.monsterList;
		}
		catch
		{
			return;
		}
		int index = FindMonster();
		if (index == -1)
		{
			gameObject.SetActive(false);
			return;
		}
		nextPos = (monsterpool[index].transform.position +
			new Vector3(0, monsterpool[index].monsterData.size) -
			gameObject.transform.position);

		float degree = Mathf.Atan2(nextPos.y, nextPos.x) * Mathf.Rad2Deg;
        gameObject.transform.eulerAngles = new Vector3(0, 0, degree);
    }
	private int FindMonster()
	{
		float distance = float.MaxValue;
		int minDisIndex = -1;
		for (int i = 0; i < monsterpool.Count; ++i)
		{
			if (monsterpool[i] == null || monsterpool[i].active == false || !monsterpool[i].gameObject.activeSelf) continue;
			else if (hitMonsterPool.Contains(i)) continue;
			float m = (gameObject.transform.position - monsterpool[i].transform.position).magnitude;
			if(distance > m)
			{
				minDisIndex = i;
				distance = m;
			}
		}
		hitMonsterPool.Add(minDisIndex);
		return minDisIndex;
	}

    private void Update()
    {
        gameObject.transform.position += gameObject.transform.right * Time.deltaTime * speed;
        StartCoroutine(Continuing());
    }
    private IEnumerator Continuing()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    public override void Crash(Monster monster)
    {
		monster.Damage(Attacktype, GameMng.Ins.player.calStat.damage, damage, new ConditionData(bufftype, Id, endTime, changeValue), per);
		GameMng.Ins.HitToEffect(Attacktype,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position,
            monster.monsterData.size);
		if (hitMonsterPool.Count == maxHitCount)
		{
			gameObject.SetActive(false);
			return;
		}
		NextPosSet();
    }
}
