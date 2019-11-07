using GlobalDefine;
using System.Collections.Generic;
using UnityEngine;

public class SkillBurningMeteor : Skill
{
	#region SkillSetting
	enum eOption
	{
		Damage,
		What,
		CoolDown,
	}

	private const float rSiz = 3;

	eAttackType attackType = eAttackType.Fire;
	private float damage;
	private string decal;
	public override void SkillSetting()
	{
		skillID = 1;
		MonsterSkillData skillData = JsonMng.Ins.monsterSkillDataTable[skillID];
		skillName = JsonMng.Ins.monsterSkillDataTable[skillID].skillName;
		decal = skillData.decal;
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eOption.Damage];
		delayTime = cooldownTime;
		target = skillData.target;
		cooldownTime = skillData.optionArr[(int)eOption.CoolDown];
		delayTime = cooldownTime;
		damage = skillData.optionArr[(int)eOption.Damage];
	}
	#endregion

    public Meteor meteor;

	private const int MaxCount = 30;
	private const int Radius = 10;
	private int Count = 0;

    public List<Meteor> MeteorList = new List<Meteor>();

	bool skillbut = false;

	private float worldtargettimer = 0.0f;

    public override void ActiveSkill()
	{
		base.ActiveSkill();
	}
	public override void SetBullet()
	{
	}
	private void FinishAttack()
	{

	}
	
	public override void SetItemBuff(eSkillOption optionType, float changeValue)
	{
	}
	private void Awake()
	{
		Targetting.SetActionDict(skillID, MeteorRun);

        for (int i = 0; i < MaxCount; ++i)
        {
            Meteor o = Instantiate(meteor,
                GameMng.Ins.player.transform.position,
                Quaternion.Euler(60, 0, 0),
                gameObject.transform);
            o.gameObject.SetActive(false);
            MeteorList.Add(o);
        }

        int j = 0;
    }

	private void Update()
	{
		if (skillbut)
			SkillStart();
	}

	public void SkillButtonOn() { skillbut = true; }
	public void SkillButtonOff() { skillbut = false; }

	void SkillStart()
	{
		if (skillbut)
			RandomTargetting();
	}

	private void RandomTargetting()
	{
		++Count;
        Vector2 mapsiz = new Vector2(DefineClass.MapSizX, DefineClass.MapSizY);

        for (int i = 0; i < MeteorList.Count; ++i)
		{
            Vector3 o = new Vector3();
            if (i == 0)
            {
                MeteorList[i].transform.position = GameMng.Ins.player.transform.position;
                GameMng.Ins.objectPool.effectPool.GetHitTargetEff(MeteorList[i].transform.position, skillID);
                if (i == Count - 1)
                    break;
                continue;
            }

            o = new Vector3((float)Rand.Range((int)-mapsiz.x, (int)mapsiz.x) / 10,
                (float)Rand.Range((int)-mapsiz.y, (int)mapsiz.y) / 10, 0);
            if ((MeteorList[i].transform.position + o).x < -DefineClass.MapSizX / 10 ||
                (MeteorList[i].transform.position + o).x > DefineClass.MapSizX / 10 ||
                (MeteorList[i].transform.position + o).y < -DefineClass.MapSizY / 10 ||
                (MeteorList[i].transform.position + o).y > DefineClass.MapSizY / 10)

            {
				--i;
				continue;
			}
            MeteorList[i].transform.position = o;
            GameMng.Ins.objectPool.effectPool.GetHitTargetEff(MeteorList[i].transform.position,skillID);

			if (i == Count - 1)
				break;
		}
        skillbut = false;
	}
    public void MeteorRun(Vector3 pos)
    {
        for(int i = 0;i<MeteorList.Count;++i)
        {
            if(!MeteorList[i].gameObject.activeSelf)
            {
                MeteorList[i].transform.position = pos;
                MeteorList[i].Setting();
                MeteorList[i].gameObject.SetActive(true);
                break;
            }
        }
    }
	public void EndEvent()
	{
		gameObject.SetActive(false);
		MeteorRun(gameObject.transform.position);
	}
}