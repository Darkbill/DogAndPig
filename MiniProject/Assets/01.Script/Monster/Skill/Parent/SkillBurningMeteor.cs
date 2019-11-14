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
		damage = skillData.optionArr[(int)eOption.Damage];
		delayTime = cooldownTime;
		cooldownTime = skillData.optionArr[(int)eOption.CoolDown];
		delayTime = cooldownTime;
		damage = skillData.optionArr[(int)eOption.Damage];
	}
	#endregion

    public Meteor meteor;

	private const int MaxCount = 10;
	private const int Radius = 10;
	private int Count;

    private float bulletradius = 0.9f;

    public List<Meteor> MeteorList = new List<Meteor>();

	bool skillbut = false;
    public override void ActiveSkill()
	{
        Count = 2;
		base.ActiveSkill();
	}
	public override void SetBullet()
	{
	}
	private void FinishAttack()
	{

	}
	public override void OffSkill()
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
            o.Setting(bulletradius, damage);
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
            if (CheckForOutMap(MeteorList[i], o, i))
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

    private bool CheckForOutMap(Meteor _meteor, Vector3 _randvec, int _index)
    {
        Vector3 pos = _meteor.transform.position + _randvec;

        if (pos.x < -DefineClass.MapSizX / 10 ||
                pos.x > DefineClass.MapSizX / 10 ||
                pos.y < -DefineClass.MapSizY / 10 ||
                pos.y > DefineClass.MapSizY / 10)
            return false;

        for(int i = 0;i<_index;++i)
        {
            if ((MeteorList[i].transform.position - pos).magnitude < bulletradius)
                return false;
        }
        return true;
    }

    public void MeteorRun(Vector3 pos)
    {
        for(int i = 0;i<MeteorList.Count;++i)
        {
            if(!MeteorList[i].gameObject.activeSelf)
            {
                MeteorList[i].transform.position = pos;
                MeteorList[i].SystemSetting();
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