using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFloorFreeze : Skill
{
    enum eFloorFreezeOption
    {
        Damage,
		Width,
		Height,
		CoolTime,
		DebufIndex,
		DebufActivePer,
		DebugEffectPer,
		DebufTime,
    }
	private float damage;
	private float width;
	private float height;
	private float DebufIndex;
	private float DebufActivePer;
	private float DebufEffectPer;
	private float DebufTime;
	

    //TODO : SpriteDummy
    public GameObject dummy;
    private float testTimer = 0.0f;

    public override void SkillSetting()
    {
        skillID = 3;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eFloorFreezeOption.Damage];
		DebufIndex = skillData.optionArr[(int)eFloorFreezeOption.DebufIndex];
		DebufActivePer = skillData.optionArr[(int)eFloorFreezeOption.DebufActivePer];
		DebufTime = skillData.optionArr[(int)eFloorFreezeOption.DebufTime];
        width = skillData.optionArr[(int)eFloorFreezeOption.Width];
        height = skillData.optionArr[(int)eFloorFreezeOption.Height];
		cooldownTime = skillData.optionArr[(int)eFloorFreezeOption.CoolTime];
		DebufEffectPer = skillData.optionArr[(int)eFloorFreezeOption.DebugEffectPer];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
	}

    public override void ActiveSkill()
    {
		base.ActiveSkill();
		dummy.GetComponentInChildren<Freezen>().Id = 3;
        dummy.GetComponentInChildren<Freezen>().MaxTimer = 3.0f;
        dummy.GetComponentInChildren<Freezen>().slow = 500f;

        dummy.SetActive(true);
        dummy.transform.position = GameMng.Ins.player.transform.position;
        dummy.transform.eulerAngles = GameMng.Ins.player.transform.eulerAngles + new Vector3(0, 0, 90);
        testTimer = 0.0f;
    }

    void Update()
    {
		delayTime += Time.deltaTime;
		if (dummy.activeSelf)
        {
            testTimer += Time.deltaTime;
            if(testTimer >= 0.5f)
                dummy.SetActive(false);
        }
    }
    //void HitSet()
    //{
    //    float range = Mathf.Sqrt(Mathf.Pow(Height, 2) / 2);
    //    Vector3 movevec = GraphParallelMovement(GameMng.Ins.player.transform.right,
    //        GameMng.Ins.player.transform.eulerAngles.z);

    //    Vector3 set = new Vector3(EssenceNormalize(GameMng.Ins.player.transform.right.x) * Height,
    //        EssenceNormalize(GameMng.Ins.player.transform.right.y) * Height,
    //        0.0f);

    //    float inclination = GameMng.Ins.player.transform.right.y / GameMng.Ins.player.transform.right.x;
    //    float inclinationInverse = movevec.y / movevec.x;


    //    for (int i = 0;i<GameMng.Ins.monsterPool.monsterList.Count;++i)
    //    {
    //        if (ObjectInCheck(movevec, 
    //                GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
    //                inclination) &&
    //            ObjectInCheckInverse(movevec * -1,
    //                GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
    //                inclination) &&
    //            ObjectInCheck(new Vector3(),
    //                GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
    //                inclinationInverse) &&
    //            (ObjectInCheckInverse(set * -1,
    //                GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
    //                inclinationInverse)))
    //        {
    //            GameMng.Ins.monsterPool.monsterList[i].Damage(eAttackType.Water, Damage);
    //        }


    //    }
    //}

    //private int EssenceNormalize(float num)
    //{
    //    if (num < 0)
    //        return -1;
    //    return 1;
    //}

    //private Vector3 GraphParallelMovement(Vector3 movVec, float angle)
    //{
    //    var inclination = movVec.x / movVec.y;
    //    float xsiz = Mathf.Cos((angle + 90) * Mathf.Deg2Rad) * Width;
    //    float ysiz = Mathf.Sqrt(Mathf.Pow(Width, 2) - Mathf.Pow(xsiz, 2));

    //    return new Vector3(xsiz, ysiz, 0);
    //}

    //private bool ObjectInCheck(Vector3 movement, Vector3 target, float inclination)
    //{
    //    float y = inclination * (target.x + movement.x) - movement.y;
    //    if (y >= 0)
    //        return true;
    //    return false;
    //}

    //private bool ObjectInCheckInverse(Vector3 movement, Vector3 target, float inclination)
    //{
    //    float y = inclination * (target.x + movement.x) - movement.y;
    //    if (y <= 0)
    //        return true;
    //    return false;
    //}
}
