using GlobalDefine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDash : Skill
{
	#region SkillSetting
	enum eDashOption
	{
		Damage,
		coolTime,
	}

	public override void SkillSetting()
	{
		skillID = 1;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		damage = skillData.optionArr[(int)eDashOption.Damage];
		cooldownTime = skillData.optionArr[(int)eDashOption.coolTime];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
	}
	public override void SetItemBuff(eSkillOption type, float changeValue)
	{
		switch (type)
		{
			case eSkillOption.Damage:
				damage += damage * changeValue;
				break;
			case eSkillOption.CoolTime:
				cooldownTime -= cooldownTime * changeValue;
				break;
		}
	}

	public override void SetBullet()
	{

	}
	public override void OffSkill()
	{

	}
	#endregion

	public Alter Bullet;
	public List<Alter> alterList = new List<Alter>();
	public GameObject StartDash;
	private bool AttackSet = false;
	private float damage;
	private const int Count = 10;
	private eAttackType attackType = eAttackType.Wind;



	List<Monster> Attack = new List<Monster>();
	public override void OnButtonDown()
	{
		ActiveSkill();
	}
	public override void ActiveSkill()
	{
		base.ActiveSkill();
		Attack.Clear();
		StartDash.transform.position = GameMng.Ins.player.transform.position;
		StartDash.GetComponent<ParticleSystem>().Play();
		//테스트 코드
		GameMng.Ins.player.playerStateMachine.ChangeState(ePlayerState.Dash);
		Vector3 direction = GameMng.Ins.player.GetForward();
		for (int i = 0; i < Count; ++i)
		{
			alterList[i].Setting(GameMng.Ins.player.transform.position + new Vector3(0.01f, 0.25f, 0), direction, i + 5);
		}
		Ray2D ray = new Ray2D(GameMng.Ins.player.transform.position, direction);
		//TODO : Range
		RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 2);
		for (int i = 0; i < hits.Length; ++i)
		{
			if (hits[i].collider.CompareTag("Monster"))
			{
				Attack.Add(hits[i].collider.GetComponent<Monster>());
			}
		}
		AttackSet = true;
	}

	private void FinishAttack()
	{
		List<Monster> hitlst = new List<Monster>();
		for (int i = 0; i < alterList.Count; ++i)
		{
			for (int j = 0; j < alterList[i].hitMonsterList.Count; ++j)
			{
				hitlst.Add(alterList[i].hitMonsterList[j]);
			}
		}
		hitlst = hitlst.Distinct().ToList();

		for (int i = 0; i < hitlst.Count; ++i)
		{
			hitlst[i].Damage(attackType, GameMng.Ins.player.calStat.damage, damage);
			GameMng.Ins.HitToEffect(attackType, hitlst[i].transform.position, hitlst[i].transform.position, 0.3f);
		}
	}

	private void Update()
	{
		delayTime += Time.deltaTime;
		int counting = 0;
		for (int i = 0; i < Count; ++i)
		{
			if (!alterList[i].gameObject.activeSelf)
				++counting;
		}
		if (counting == Count - 1 && AttackSet)
		{
			FinishAttack();
			AttackSet = false;
		}
	}
}
