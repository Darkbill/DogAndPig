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
	private float buffActivePer;

    private const float splitCnt = 4;


	List<Lightning> lightningList = new List<Lightning>();

    public void Setting(int id, int splitcnt, float p, float damage,float _buffEndTime)
    {
        Id = id;
        per = p;
        SetTimer = 0.0f;
        SplitCnt = splitcnt;
        MaxTimer = SplitCnt;
        Speed = (Rand.Random() % 10 / 3 + 1f) / 2;
        SetTimer = 0.0f;
		buffEndTime = _buffEndTime;
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
                Lightning light = Instantiate(
                    this,
                    gameObject.transform.position,
                    Quaternion.Euler(0, 0, 180 * 2 / 4 * Count + randnum));
                light.transform.parent = GameMng.Ins.skillMng.transform;
                light.Setting(Id, SplitCnt - 1, per, damage, buffEndTime);
                lightningList.Add(light);
                ++Count;
            }
            if (!lightningList[i].gameObject.activeSelf)
            {
                lightningList[i].transform.position = gameObject.transform.position;
                lightningList[i].transform.rotation = Quaternion.Euler(0, 0, 180 * 2 / 4 * Count + randnum);
                lightningList[i].Setting(Id, SplitCnt - 1, per, damage, buffEndTime);
                lightningList[i].gameObject.SetActive(true);
                ++Count;
            }
        }
    }
}
