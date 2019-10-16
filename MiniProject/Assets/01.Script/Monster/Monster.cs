using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class Monster : MonoBehaviour
{
	public MonsterData monsterData;

	/* 테스트코드 */
	private int MonsterID = 1;
	private List<ConditionData> conditionList = new List<ConditionData>();
	public void Start()
	{
		MonsterSetting();
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
	}
	public void MonsterSetting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[MonsterID].Copy();
	}

	/* 테스트코드 */
	public virtual void Attack()
	{
		GameMng.Ins.DamageToPlayer(eAttackType.Physics, monsterData.damage);
	}

    #region MonsterDamageSet
    public void Damage(eAttackType attackType, float damage)
	{
        float d = (damage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float damage, ConditionData condition,float activePer)
	{
		float d = (damage - monsterData.armor) * monsterData.GetResist(attackType).CalculatorDamage();
		bool isBuff = monsterData.GetResist(attackType).GetBuff(activePer);
		if (isBuff)
		{
			AddBuff(condition);
            if (condition.buffType == eBuffType.MoveSlow)
            {
                //condition.changeValue;//감소값
            }
            if(condition.buffType == eBuffType.Stun)
            {
                //condition.currentTime;//기절시간.
                if(condition.changeValue < Rand.Random() % 1000)//확률값
                    gameObject.GetComponent<MilliMonsterStateMachine>().ChangeState(eMilliMonsterState.Stun);
            }
			CalculatorStat();
		}
		DamageResult((int)d);
	}
    #endregion
    public void ConditionUpdate()
    {

    }
    public void KnockBackAttack(float Range)
    {
        //TODO : Test KnockBack
        //Range는 뒤로 밀려나는 거리
        gameObject.GetComponent<MilliMonsterStateMachine>().ChangeState(eMilliMonsterState.KnockBack);
    }

	public void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
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
		CalculatorStat();
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
		Debug.Log("죽었다");
        UIMngInGame.Ins.OnCoinSelectInGame(2);
		//TODO : 경험치 추가
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
            gameObject.GetComponent<MilliMonsterStateMachine>().
                ChangeState(eMilliMonsterState.Idle);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            gameObject.GetComponent<MilliMonsterStateMachine>().
                ChangeState(eMilliMonsterState.Idle);
        }
    }

}
