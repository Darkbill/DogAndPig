using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBurningMeteor : Skill
{
    #region SkillSetting
    enum eDashOption
    {
        Damage,
    }

    private const float rSiz = 3;

    eAttackType attackType = eAttackType.Fire;
    private float damage;
    public override void SkillSetting()
    {
        skillID = 6;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eDashOption.Damage];
        delayTime = cooldownTime;
    }
    #endregion

    public Targetting Target;

    private const int MaxCount = 30;
    private const int Radius = 10;
    private int Count = 0;
    public List<Targetting> TargetList = new List<Targetting>();

    bool skillbut = false;

    private float worldtargettimer = 0.0f;

    public override void ActiveSkill()
    {
        base.ActiveSkill();
    }

    private void FinishAttack()
    {

    }

    private void Awake()
    {
        for(int i = 0;i<MaxCount;++i)
        {
            Targetting o = Instantiate(Target,
                GameMng.Ins.player.transform.position,
                Quaternion.Euler(0, 0, 0),
                gameObject.transform);
            o.gameObject.SetActive(false);
            TargetList.Add(o);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("z"))
            skillbut = true;
        if (skillbut)
            SkillStart();
    }

    void SkillStart()
    {
        worldtargettimer += Time.deltaTime;
        if (worldtargettimer >= 3.0f)
        {
            RandomTargetting();
            worldtargettimer = 0.0f;
        }
    }

    private void RandomTargetting()
    {
        ++Count;
        for (int i = 0; i < TargetList.Count; ++i)
        {
            TargetList[i].Setting();

            Vector3 o = new Vector3((float)Rand.Range(-Radius, Radius) / 10, (float)Rand.Range(-Radius, Radius) / 10, 0);
            if ((TargetList[i].transform.position + o).x < -DefineClass.MapSizX / 10 ||
               (TargetList[i].transform.position + o).x > DefineClass.MapSizX / 10 ||
               (TargetList[i].transform.position + o).y < -DefineClass.MapSizY / 10 ||
               (TargetList[i].transform.position + o).y > DefineClass.MapSizY / 10)
            {
                --i;
                continue;
            }
            TargetList[i].transform.position += o;
            TargetList[i].gameObject.SetActive(true);
            if (i == Count - 1)
                break;
        }
    }
}
