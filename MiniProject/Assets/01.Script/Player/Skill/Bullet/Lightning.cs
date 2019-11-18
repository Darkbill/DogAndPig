using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;
public class Lightning : BulletPlayerSkill
{
    public float damage = 0;
	eAttackType Attacktype = eAttackType.Lightning;
	eBuffType bufftype = eBuffType.Stun;

	private int Id;
    private float MaxTimer = 0.0f;
    public float SetTimer = 0.0f;
    private float per;

    public Vector3 MoveVec;
    public Vector3 EndPos;
    public int SplitCnt;
    public float Speed;
	private float buffEndTime;

    private const float splitCnt = 4;
	List<Lightning> lightningList = new List<Lightning>();

    public void Setting(int id, float p, float damage,float _buffEndTime)
    {
        Id = id;
        per = p;
		buffEndTime = _buffEndTime;
		gameObject.transform.parent = GameMng.Ins.skillMng.transform;
		gameObject.SetActive(false);
	}
	public void SystemSetting(Vector3 pos,Quaternion angle, int splitcnt)
	{
        Speed = (Rand.Random() % 10 / 3 + 1f) / 2;
        gameObject.transform.position = pos;
		gameObject.transform.rotation = angle;
        SplitCnt = splitcnt;
        MaxTimer = SplitCnt;
        SetTimer = 0.0f;
        gameObject.SetActive(true);
	}
    private void Update()
    {
        SetTimer += Time.deltaTime;
        gameObject.transform.position += gameObject.transform.right * Time.deltaTime * Speed;
        if (SetTimer >= MaxTimer)
            gameObject.SetActive(false);
    }
	public override void Crash(Monster monster)
	{
        if (SetTimer > 0.5f)
        {
            monster.Damage(Attacktype, GameMng.Ins.player.calStat.damage,damage);
			GameMng.Ins.HitToEffect(Attacktype, 
                monster.transform.position + new Vector3(0, monster.monsterData.size), 
                gameObject.transform.position,
                monster.monsterData.size);
			if (Rand.Permile(per)) monster.OutStateAdd(new ConditionData(bufftype, Id, buffEndTime, 0));
            EndPos = monster.transform.position;
            int randnum = Rand.Random() % 90;
            CreateAndPoolBullet(randnum);
            gameObject.SetActive(false);
        }
	}

    private void CreateAndPoolBullet(int randnum)
    {
        int Count = 0;
        for (int i = 0; Count < splitCnt; ++i)
        {
            if (lightningList.Count == i)
            {
                GameObject light = Instantiate(gameObject, GameMng.Ins.skillMng.transform);
                light.GetComponent<Lightning>().Setting(Id, per, damage, buffEndTime);
                light.GetComponent<Lightning>().SystemSetting(gameObject.transform.position,
                                    Quaternion.Euler(0, 0, 180 * 2 / 4 * Count + randnum),
                                    SplitCnt - 1);
                lightningList.Add(light.GetComponent<Lightning>());
                ++Count;
            }
            if (!lightningList[i].gameObject.activeSelf)
            {
                lightningList[i].SystemSetting(gameObject.transform.position, 
                    Quaternion.Euler(0, 0, 180 * 2 / 4 * Count + randnum), 
                    SplitCnt - 1);
                ++Count;
            }
        }
    }
    
    public void OffSkillSet()
    {
        foreach (Lightning o in lightningList)
            o.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
