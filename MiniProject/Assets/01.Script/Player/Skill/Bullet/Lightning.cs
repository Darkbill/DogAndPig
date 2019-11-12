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
    public bool SplitCheck = false;
    public int SplitCnt;
    public float Speed;
	private float buffEndTime;
	private float buffActivePer;


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
        if (SetTimer >= MaxTimer)
            gameObject.SetActive(false);
    }
	public override void Crash(Monster monster)
	{
        if (SetTimer > 0.3f)
        {
            monster.Damage(Attacktype, GameMng.Ins.player.calStat.damage,damage);
			GameMng.Ins.HitToEffect(Attacktype, 
                monster.transform.position + new Vector3(0, monster.monsterData.size), 
                gameObject.transform.position,
                monster.monsterData.size);
			if (Rand.Permile(per)) monster.OutStateAdd(new ConditionData(bufftype, Id, buffEndTime, 0));
            EndPos = monster.transform.position;
            SplitCheck = true;
            gameObject.SetActive(false);
        }
	}
}
