using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkIce : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Water;
    eBuffType bufftype = eBuffType.MoveSlow;

    private int Id = 10;
    private float per;

    private Vector3 nextPos;

    private int idxnum = 0;

    private List<Monster> monsterpool;

    public void Setting(int id, float p, float damage)
    {
        Id = id;
        per = p;
        gameObject.transform.position = GameMng.Ins.player.transform.position +
            new Vector3(0, GameMng.Ins.player.calStat.size);
        monsterpool = GameMng.Ins.monsterPool.monsterList;
        NextPosSet();
        idxnum = 0;
    }

    private void NextPosSet()
    {
        nextPos = GameMng.Ins.player.transform.right;

        for (int i = idxnum; i < monsterpool.Count; ++i)
        {
            if (monsterpool[i] == null)
                continue;
            nextPos = (monsterpool[idxnum].transform.position +
                        new Vector3(0, monsterpool[idxnum].monsterData.size) -
                        gameObject.transform.position);
            break;
        }
        
        float degree = Mathf.Atan2(nextPos.y, nextPos.x) * Mathf.Rad2Deg;
        gameObject.transform.eulerAngles = new Vector3(0, 0, degree);

        ++idxnum;
    }

    private void Update()
    {
        if (idxnum >= monsterpool.Count)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.transform.position += gameObject.transform.right * Time.deltaTime * 10;

    }

    public override void Crash(Monster monster)
    {
        NextPosSet();
        monster.Damage(Attacktype, damage);
        monster.OutStateAdd(new ConditionData(bufftype, Id, 10.0f, 500), 1000);
    }
}
