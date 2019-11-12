using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;

public class Monster : MonoBehaviour
{
	public MonsterData monsterData;
	public StateMachine StateMachine;
	private List<ConditionData> conditionList = new List<ConditionData>();
	private ConditionData conditionMain = new ConditionData();
	public Animator monsterAnimator;
	protected ObjectSetAttack att = new ObjectSetAttack();
	public float Angle = 0;
	public bool active;
	public int MonsterID = 1;
	public float delayTime = 0;
	private void Awake()
	{
		MonsterSetting();
	}

	private void MonsterSetting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[MonsterID].Copy();
		active = true;
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
		delayTime += Time.deltaTime;
		if (Angle > 180) { Angle -= 360; }
		else if (Angle < -180) { Angle += 360; }

		if (Angle > -90 && Angle <= 90)
			gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
		else
			gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
	}
	#region StateCheck
	public bool AttackDelayCheck()
	{
		if (delayTime >= Define.standardAttackSpeed / monsterData.attackSpeed)
		{
			return true;
		}
		return false;
	}
	public virtual bool AttackCheck()
	{
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (att.BaseAttack(GetForward(), directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
		{
			return true;
		}
		return false;
	}
	public bool AttackDistanceCheck()
	{
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (directionToPlayer.magnitude <= monsterData.attackRange)
		{
			return true;
		}
		return false;
	}
	#endregion

	#region Buff
	public void OutStateAdd(ConditionData condition, Vector3 knockBackDir = new Vector3())
	{
		conditionMain = condition;
		AddConditionlist(conditionMain, knockBackDir);
	}

	private void AddConditionlist(ConditionData condition, Vector3 knockBackDir = new Vector3())
	{
		switch (condition.buffType)
		{
			case eBuffType.Stun:
				StateMachine.ChangeStateStun();
				GameMng.Ins.monsterPool.SelectEffect(gameObject, condition);
				break;
			case eBuffType.NockBack:
				StateMachine.ChangeStateKnockBack(knockBackDir, condition.changeValue);
				break;
		}
	}

	private void UpdateBuff(float delayTime)
	{
		for (int i = 0; i < conditionList.Count; ++i)
		{
			conditionList[i].currentTime -= delayTime;
			if (conditionList[i].currentTime <= 0)
			{
				conditionList.Remove(conditionList[i]);
				CalculatorStat();
			}
		}
	}
	public void AddBuff(ConditionData condition)
	{
		int index = conditionList.FindID(condition.skillIndex, condition.buffType);
		if (index != -1)
		{
			conditionList[index].Set(condition);
			return;
		}
		else conditionList.Add(condition);
		GameMng.Ins.monsterPool.SelectEffect(gameObject, condition);

		CalculatorStat();

	}
	#endregion
	#region MonsterDamageSet
	public void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float damage, float skillDamage)
	{
		float d = (damage + skillDamage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float PlayerDmage, float skillDamage, ConditionData condition, float activePer)
	{
		float d = (PlayerDmage + skillDamage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		bool isBuff = monsterData.GetResist(attackType).GetBuff(activePer);
		if (isBuff)
		{
			AddBuff(condition);
			CalculatorStat();
		}
		DamageResult((int)d);
	}
	public virtual void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.damageTextPool.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
		GameMng.Ins.cameraMove.OnStart();
	}
	public virtual void Dead()
	{
		//TODO : RunningSelect
		//1: Type, 2:Count, 3:StartPos
		GameMng.Ins.objectPool.goodmng.RunningSelect(1, 10, gameObject.transform.position);
		StateMachine.ChangeStateDead();
		active = false;
		ColliderOnOff(false);
		GameMng.Ins.MonsterDead();
		GameMng.Ins.AddEXP(GameMng.stageLevel);
	}
	#endregion

	private void CalculatorStat()
	{
		float cHP = monsterData.healthPoint;
		MonsterSetting();
		monsterData.healthPoint = cHP;
		for (int i = 0; i < conditionList.Count; ++i)
		{
			switch (conditionList[i].buffType)
			{
				case eBuffType.MoveFast:
					monsterData.moveSpeed += monsterData.moveSpeed * conditionList[i].changeValue;
					break;
				case eBuffType.MoveSlow:
					monsterData.moveSpeed -= monsterData.moveSpeed * conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsStrong:
					monsterData.damage += monsterData.damage * conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsWeek:
					monsterData.damage -= monsterData.damage * conditionList[i].changeValue;
					break;
			}
		}
	}


	public void ColliderOnOff(bool check)
	{
		gameObject.GetComponent<CircleCollider2D>().enabled = check;
	}

	public void ChangeAnimation(eMonsterAnimation animationType)
	{
		monsterAnimator.SetInteger("Action", (int)animationType);
		SetAnimationSpeed(animationType);
	}
	public void SetAnimationSpeed(eMonsterAnimation animationType)
	{
		switch (animationType)
		{
			case eMonsterAnimation.Attack:
				monsterAnimator.speed = monsterData.attackSpeed * Define.standardAttackSpeed;
				break;
			case eMonsterAnimation.Run:
				monsterAnimator.speed = monsterData.moveSpeed * Define.standardMoveSpeed;
				break;
			default:
				monsterAnimator.speed = 1;
				break;
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Bullet"))
		{
			collision.GetComponent<BulletPlayerSkill>().Crash(this);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Monster"))
		{
			ZigMonsterAngle();
		}
	}
	private void ZigMonsterAngle()
	{
		Vector3 playerpos = GameMng.Ins.player.transform.position;

		float range = (gameObject.transform.position - playerpos).magnitude;
		if (range > 2) return;
		if (range < monsterData.attackRange)
			return;
		else
			Angle -= 90;
	}

	public void ActiveOff()
	{
		//애니메이션 호출 함수
		Destroy(gameObject);
	}

	public ConditionData ConditionMainGet() { return conditionMain; }

	/* 계산용 */
	public Vector3 GetForward()
	{
		return new Vector3(Mathf.Cos(Angle * Mathf.Deg2Rad), Mathf.Sin(Angle * Mathf.Deg2Rad), 0);
	}
}
