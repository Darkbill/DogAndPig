using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTripleAttack : Skill
{
    #region SkillSetting
    enum eOption
    {
        Damage,
        What,
        CoolDown,
        Radian
    }

    private float damage;
    private string decal;
    private float radian;
    public override void SkillSetting()
    {
        skillID = 4;
        MonsterSkillData skillData = JsonMng.Ins.monsterSkillDataTable[skillID];
        skillName = JsonMng.Ins.monsterSkillDataTable[skillID].skillName;
        decal = skillData.decal;
        damage = skillData.optionArr[(int)eOption.Damage];
        delayTime = cooldownTime;
        cooldownTime = skillData.optionArr[(int)eOption.CoolDown];
        radian = skillData.optionArr[(int)eOption.Radian];
        delayTime = cooldownTime;
        damage = skillData.optionArr[(int)eOption.Damage];
    }
    #endregion

    //public Meteor meteor;

    private const float endTime = 5.0f;
    private const float degree = 45;
    private const float speed = 3;
    private float[] settingArray = new float[] { -50, -30, -10, 10, 30, 50 };

    private float bulletradius = 0.9f;

    public List<TripleAttackBullet> bullet = new List<TripleAttackBullet>();
    public override void ActiveSkill()
    {
        base.ActiveSkill();
    }
    public override void SetBullet()
    {
    }
    public override void OffSkill()
    {
    }

    private void Awake()
    {
        for (int i = 0; i < bullet.Count; ++i)
            bullet[i].Setting(skillID, damage, speed, endTime);
    }

    private void Update()
    {
    }

    public void SkillStart(Monster monster)
    {
        AttackRun(monster);
    }

    private void AttackRun(Monster monster)
    {
        int count = 0;

        float[] array = new float[6];
        int idx = 0;
        foreach(float arr in settingArray)
        {
            array[idx] = arr + monster.Angle;
            ++idx;
        }

        StartCoroutine(TripleAttackStart(array, monster.transform.position));
    }

    private IEnumerator TripleAttackStart(float[] array, Vector3 pos)
    {
        bulletSetTrue(0, 4, array, pos, 0);
        yield return new WaitForSeconds(0.5f);
        bulletSetTrue(2, 6, array, pos, -10);
        yield return new WaitForSeconds(0.5f);
        bulletSetTrue(0, 6, array, pos, 0);
    }

    private void bulletSetTrue(int _count, int max, float[] array, Vector3 pos, float changeAngle)
    {
        int count = _count;
        foreach (TripleAttackBullet o in bullet)
        {
            if (count >= max) break;
            if (!o.gameObject.activeSelf)
            {
                o.SystemSetting(array[count] + changeAngle, pos);
                ++count;
            }
        }
        for(int i = count;i<max;++i)
        {
            TripleAttackBullet o = Instantiate(bullet[0]);
            o.Setting(skillID, damage, speed, endTime);
            o.SystemSetting(array[count], pos);
            bullet.Add(o);
        }
    }


    public override void SetItemBuff(eSkillOption optionType, float changeValue)
    {
        throw new System.NotImplementedException();
    }
}
