using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
using System;

public class Monster : MonoBehaviour
{
    enum ChangeMainCondition
    {
        None = 0,
        Stun,
        KnockBack,
        Max,
    }

	public MonsterData monsterData;

	/* 테스트코드 */
	private int MonsterID = 1;
	private List<ConditionData> conditionList = new List<ConditionData>();
    private List<ConditionData> conditionMainList = new List<ConditionData>();
    //private ConditionData conditionMain = new ConditionData();
	public void Start()
	{
		MonsterSetting();
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
        OutStateUpdate(Time.deltaTime);
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
            CalculatorStat();
		}
		DamageResult((int)d);
	}
    //TODO : State 따로관리 stun과 nockback
    //add에서 상태이상 비교하여 삽입.
    public void OutStateAdd(ConditionData condition, float activePer)
    {  
        //TODO : condition이 하나일때
        //if(conditionMain.buffType == condition.buffType)
        //{
        //    if(conditionMain.currentTime < condition.currentTime)
        //    {
        //        conditionMain = condition;
        //        AddConditionlist(conditionMain);
        //    }
        //    return;
        //}
        //conditionMain = condition;
        //AddConditionlist(conditionMain);
        for(int i = 0;i<conditionMainList.Count;++i)
        {
            //요기에 수준값을 비교해서 기존 삭제하면됨. 수준값은 일단 시간으로 설정.(남은 시간 기준)
            if (conditionMainList[i].buffType == condition.buffType &&
                conditionMainList[i].currentTime < condition.currentTime)
            {
                conditionMainList.Remove(conditionMainList[i]);
                conditionMainList.Add(condition);
                AddConditionlist(condition);
                return;
            }
            else if (conditionMainList[i].buffType == condition.buffType &&
                conditionMainList[i].currentTime >= condition.currentTime)
                return;
        }
        AddConditionlist(condition);
    }

    private void AddConditionlist(ConditionData condition)
    {
        conditionMainList.Add(condition);
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

    //TODO : 현재 상태이상에 대한 처리..일반버프랑 유사한 구조.
    //전부 풀려야 가능한 구조.. 즉 최종으로 들어온 값에 따라 해제의 여부가 갈라진다.
    //만약 스턴이 걸린상태에서 넉백으로 초기화되는 경우라면 리스트가 아닌 변수 하나로 퉁치고
    //넉백이 걸린 상태에서 다시 넉백이 들어오는 경우 수준값을 비교하여 초기화 및 대입하면 될듯?
    //아 힐링하고싶다
    public void OutStateUpdate(float delayTime)
    {
        //conditionMain.currentTime -= delayTime;
        //if (conditionMain.currentTime <= 0 && conditionMain != null)
        //{
        //    Debug.Log("상태이상이 풀렸습니다.");
        //    conditionMain = null;
        //    gameObject.GetComponent<MilliMonsterStateMachine>().ChangeState(eMilliMonsterState.Move);
        //}

        bool RemoveCheck = false;
        for (int i = 0;i<conditionMainList.Count;++i)
        {
            conditionMainList[i].currentTime -= delayTime;
            if (conditionMainList[i].currentTime <= 0)
            {
                Debug.Log(i + "번째의 상태이상이 풀렸습니다.");
                conditionMainList.Remove(conditionMainList[i]);
                RemoveCheck = true;
            }
        }
        if(conditionMainList.Count == 0 && RemoveCheck)
            gameObject.GetComponent<MilliMonsterStateMachine>().ChangeState(eMilliMonsterState.Move);
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
    #endregion
    public void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
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
