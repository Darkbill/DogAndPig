using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
	public MonsterData monsterData;
	public MonsterStateMachine monsterStateMachine;
	/* 테스트코드 */
	public int MonsterID = 1;
	private List<ConditionData> conditionList = new List<ConditionData>();
    private ConditionData conditionMain = new ConditionData();
    public Animator monsterAnimator;

    public float Angle = 0;
    public Vector3 right = new Vector3(1, 0, 0);

    public int actionNum = 0;

	private void Awake()
	{
		MonsterSetting();

    }
	private void MonsterSetting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[MonsterID].Copy();
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);

        if (Angle > 180) { Angle -= 360; }
        else if (Angle < -180) { Angle += 360; }

        if (Angle > -90 && Angle <= 90)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        else
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
    }   

	/* 테스트코드 */
	public virtual void Attack()
	{
		GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
	}
	#region Buff
	//TODO : State 따로관리 stun과 nockback
	//add에서 상태이상 비교하여 삽입.
	public void OutStateAdd(ConditionData condition, float activePer)
	{
		if (conditionMain != null)
		{
			if (conditionMain.buffType == condition.buffType)
			{
				if (conditionMain.currentTime < condition.currentTime)
				{
					conditionMain = condition;
					AddConditionlist(conditionMain);
				}
				return;
			}
		}
		conditionMain = condition;
		AddConditionlist(conditionMain);
	}

	private void AddConditionlist(ConditionData condition)
	{
		switch (condition.buffType)
		{
			case eBuffType.Stun:
				gameObject.GetComponent<MilliMonsterStateMachine>().ChangeState(eMilliMonsterState.Stun);
				break;
			case eBuffType.NockBack:
				gameObject.GetComponent<MilliMonsterStateMachine>().ChangeState(eMilliMonsterState.KnockBack);
				break;
		}
		Debug.Log("상태이상에 걸렸습니다.");
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
		if (conditionMain != null)
		{
			conditionMain.currentTime -= delayTime;
			if (conditionMain.currentTime <= 0)
			{
				Debug.Log("상태이상이 풀렸습니다.");
				conditionMain = null;
				gameObject.GetComponent<MilliMonsterStateMachine>().ChangeState(eMilliMonsterState.Move);
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
		CalculatorStat();
	}
#endregion
	#region MonsterDamageSet
	public void Damage(eAttackType attackType, float damage)
	{
        float d = (damage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float PlayerDmage, float skillDamage, ConditionData condition,float activePer)
	{
		float d = (PlayerDmage +  skillDamage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		bool isBuff = monsterData.GetResist(attackType).GetBuff(activePer);
		if (isBuff)
		{
			AddBuff(condition);
            CalculatorStat();
		}
		DamageResult((int)d);
	}
    
    #endregion
    public void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
		else
		{
			ChangeAnimation(eMonsterAnimation.Hit);
		}
	}

	
	private void CalculatorStat()
	{
		MonsterSetting();
		for (int i = 0; i < conditionList.Count; ++i)
		{
			switch (conditionList[i].buffType)
			{
				case eBuffType.MoveFast:
					monsterData.moveSpeed += conditionList[i].changeValue;
					break;
				case eBuffType.MoveSlow:
					monsterData.moveSpeed -= conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsStrong:
					monsterData.damage += conditionList[i].changeValue;
					break;
				case eBuffType.PhysicsWeek:
					monsterData.damage -= conditionList[i].changeValue;
					break;
			}
		}
	}
	public virtual void Dead()
	{
		ChangeAnimation(eMonsterAnimation.Dead);
		monsterStateMachine.ChangeStateDead();
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		GameMng.Ins.AddGold(GameMng.Ins.stageLevel);
		GameMng.Ins.AddEXP(GameMng.Ins.stageLevel);
	}

    public void ChangeAnimation(eMonsterAnimation animationType)
    {
        monsterAnimator.SetInteger("Action", (int)animationType);
    }

	//TODO : 넉백관련 몬스터 코드는 일단 주석처리..
	/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
            gameObject.GetComponent<MilliMonsterStateMachine>().
                ChangeState(eMilliMonsterState.Move);
        }
    }

    //*/
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Bullet"))
		{
			collision.GetComponent<BulletPlayerSkill>().Crash(this);
		}
	}
	public void ActiveOff()
	{
		gameObject.SetActive(false);
	}
}
