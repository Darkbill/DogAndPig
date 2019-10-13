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
        DebufPer,
        DebufMinus,
        DebufTime,
        Width,
        Height,
        Times
    }
    private float damage = 1;
    private float debufPer = 100;
    private float debufMinus = 0;
    private float debufTime = 0;
    private float width = 1;
    private float height = 2;
    private float times = 2;

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
        debufPer = skillData.optionArr[(int)eFloorFreezeOption.DebufPer];
        debufMinus = skillData.optionArr[(int)eFloorFreezeOption.DebufMinus];
        debufTime = skillData.optionArr[(int)eFloorFreezeOption.DebufTime];
        width = skillData.optionArr[(int)eFloorFreezeOption.Width];
        height = skillData.optionArr[(int)eFloorFreezeOption.Height];
        times = skillData.optionArr[(int)eFloorFreezeOption.Times];
        //dummy.GetComponentInChildren<Freezen>().transform.localScale = new Vector3(10 * width, height * 3.0f / 75, 0);
        //dummy.GetComponentInChildren<Freezen>().transform.position = new Vector3(0, -height / 2 / 75, 0);
    }

    public override void ActiveSkill()
    {
        gameObject.SetActive(true);
        dummy.GetComponentInChildren<Freezen>().Id = 3;
        dummy.GetComponentInChildren<Freezen>().MaxTimer = 3.0f;
        dummy.GetComponentInChildren<Freezen>().slow = 500f;

        dummy.SetActive(true);
        dummy.transform.position = GameMng.Ins.player.transform.position;
        dummy.transform.eulerAngles = GameMng.Ins.player.transform.eulerAngles + new Vector3(0, 0, 90);
        testTimer = 0.0f;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
