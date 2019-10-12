using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFloorFreeze : MonoBehaviour
{

    private float Damage = 10;
    private float DebufPer = 100;
    //private float DebufMinus = 0;
    //private float DebufTime = 0;
    private float Width = 1;
    private float Height = 2;
    private float Times = 0;

    //TODO : SpriteDummy
    public GameObject dummy;
    private float testTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            HitSet();
            dummy.SetActive(true);
            dummy.transform.eulerAngles = GameMng.Ins.player.transform.eulerAngles + new Vector3(0, 0, 90);
            dummy.transform.position = GameMng.Ins.player.transform.position;
        }
        if(dummy.activeSelf)
        {
            testTimer += Time.deltaTime;
            if(testTimer > 0.5f)
            {
                dummy.SetActive(false);
                testTimer = 0.0f;
            }
        }
    }
    
    void HitSet()
    {
        Vector3 movevec = GraphParallelMovement(GameMng.Ins.player.transform.right,
            GameMng.Ins.player.transform.eulerAngles.z);

        float inclination = GameMng.Ins.player.transform.right.y / GameMng.Ins.player.transform.right.x;
        float inclinationInverse = movevec.y / movevec.x;
        float range = Mathf.Sqrt(Mathf.Pow(Height, 2) / 2);

        for (int i = 0;i<GameMng.Ins.monsterPool.monsterList.Count;++i)
        {
            if (ObjectInCheck(movevec, 
                    GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
                    inclination) &&
                ObjectInCheckInverse(movevec * -1,
                    GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
                    inclination) &&
                ObjectInCheck(new Vector3(),
                    GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
                    inclinationInverse) &&
                (ObjectInCheckInverse(new Vector3(range, range, 0) * -1,
                    GameMng.Ins.monsterPool.monsterList[i].transform.position - GameMng.Ins.player.transform.position,
                    inclinationInverse)))
            {
                GameMng.Ins.monsterPool.monsterList[i].Damage(eAttackType.Water, Damage);
            }
            

        }
    }

    private Vector3 GraphParallelMovement(Vector3 movVec, float angle)
    {
        var inclination = movVec.x / movVec.y;
        float xsiz = Mathf.Cos((angle + 90) * Mathf.Deg2Rad) * Width;
        float ysiz = Mathf.Sqrt(Mathf.Pow(Width, 2) - Mathf.Pow(xsiz, 2));

        return new Vector3(xsiz, ysiz, 0);
    }

    private bool ObjectInCheck(Vector3 movement, Vector3 target, float inclination)
    {
        float y = inclination * (target.x + movement.x) - movement.y;
        if (y <= 0)
            return true;
        return false;
    }

    private bool ObjectInCheckInverse(Vector3 movement, Vector3 target, float inclination)
    {
        float y = inclination * (target.x + movement.x) - movement.y;
        if (y >= 0)
            return true;
        return false;
    }
}
