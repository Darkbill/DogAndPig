﻿using GlobalDefine;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	// * public * //
	[HideInInspector]
	public bool isMove;
	[HideInInspector]
	public bool isAim;
	public PlayerData calStat;
	public PlayerStateMachine playerStateMachine;
	public Animator playerAnimator;
	public float degree;
	// * private * //
	private List<ConditionData> conditionList = new List<ConditionData>();
    private ConditionData conditionMain = new ConditionData();
	private PlayerData skillStat = new PlayerData();
	private ObjectSetAttack att = new ObjectSetAttack();
	private int exp = 0;
    private Vector3 playerMonsterAutoAttackVec = Vector3.right;


	public void PlayerSetting()
	{
		calStat = JsonMng.Ins.playerDataTable[1];
		CalculatorStat();
	}
	public void CalculatorStat()
	{
		calStat = JsonMng.Ins.playerDataTable[1].AddStat(skillStat, conditionList, calStat.level);
	}
	public void CalculatorBuffStat()
	{
		//HP를 제외한 값만 초기 데이터에서 불러오기 위함
		float cHp = calStat.healthPoint;
		CalculatorStat();
		calStat.healthPoint = cHp;
	}
	public void ChangePlayerAngle()
	{
		if (playerStateMachine.isAttack == false)
		{
			if (Mathf.Abs(degree) != 90)
			{
				if (degree > 90 || degree < -90)
				{
					transform.eulerAngles = new Vector3(0, 180, 0);
				}
				else
				{
					transform.eulerAngles = new Vector3(0, 0, 0);
				}
			}
		}
		else
		{
			AttackCheck();
		}
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
		ChangePlayerAngle();

        var monsterPool = GameMng.Ins.monsterPool.monsterList;
        playerMonsterAutoAttackVec = att.GetFindShortStreet(monsterPool,
            gameObject.transform.position,
            calStat.attackRange + 0.3f,
            gameObject.transform.right.x);

        Debug.DrawRay(gameObject.transform.position + new Vector3(0, 0.3f, 0), playerMonsterAutoAttackVec);
    }
	#region Damage
	/* Damage */
	public void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - calStat.armor) * calStat.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float damage, ConditionData condition,float activePer)
	{
		float d = (damage - calStat.armor) * calStat.GetResist(attackType).CalculatorDamage();
		bool isBuff = calStat.GetResist(attackType).GetBuff(activePer);
		if(isBuff)
		{
			AddBuff(condition);
		}
		DamageResult((int)d);
	}
	public void DamageResult(int d)
	{
		if (d < 1) d = 1;
		calStat.healthPoint -= d;
		if (calStat.healthPoint <= 0)
		{
			calStat.healthPoint = 0;
			ChangeAnimation(ePlayerAnimation.Dead);
			playerStateMachine.ChangeState(ePlayerState.Dead);
			GameMng.Ins.GameOver();
		}
		UIMngInGame.Ins.DamageToPlayer(d);
	}
	/* Damage */
	#endregion
	#region Buff
	/* Buff */
	private void UpdateBuff(float delayTime)
	{
		for (int i = 0; i < conditionList.Count; ++i)
		{
			conditionList[i].currentTime -= delayTime;
			if (conditionList[i].currentTime <= 0)
			{
				conditionList.Remove(conditionList[i]);
				CalculatorBuffStat();
			}
		}
		if (conditionMain != null)
		{
			conditionMain.currentTime -= delayTime;
			if (conditionMain.currentTime <= 0)
			{
				Debug.Log("상태이상이 풀렸습니다.");
				conditionMain = null;
				playerStateMachine.ChangeState(ePlayerState.Move);
			}
		}
	}

	public void AddBuff(ConditionData condition)
	{
        int index = conditionList.FindID(condition.skillIndex, condition.buffType);
        if (index != -1) { conditionList[index].Set(condition); return; }
        else conditionList.Add(condition);
		CalculatorBuffStat();
		UIMngInGame.Ins.ActiveBuff(condition.skillIndex);
	}
	
    public void OutStateAdd(ConditionData condition, float activePer)
    {
        if (conditionMain != null)
        {
            if (conditionMain.buffType == condition.buffType)
            {
                if (conditionMain.currentTime < condition.currentTime)
                {
                    conditionMain = condition;
					ChangeState(conditionMain.buffType);
                }
                return;
            }
        }
        conditionMain = condition;
		ChangeState(conditionMain.buffType);
    }

    private void ChangeState(eBuffType stateType)
    {
		switch(stateType)
		{
			case eBuffType.NockBack:
				playerStateMachine.ChangeState(ePlayerState.KnockBack);
				break;
			case eBuffType.Stun:
				playerStateMachine.ChangeState(ePlayerState.Stun);
				break;
		}
        Debug.Log("상태이상에 걸렸습니다.");
    }


	/* Buff */
	#endregion
	public void AddEXP(int _exp)
	{
		exp += _exp;
		if(exp >= JsonMng.Ins.expDataTable[calStat.level+1].requiredExp)
		{
			int saveEXP = JsonMng.Ins.expDataTable[calStat.level + 1].requiredExp - exp;
			LevelUP();
			exp = saveEXP;
		}
	}

	private void LevelUP()
	{
		calStat.level++;
		exp = 0;
		CalculatorStat();
		UIMngInGame.Ins.RenewPlayerInfo();
	}
	public void ChangeAnimation(ePlayerAnimation animationType)
	{
		if (playerStateMachine.isAttack == true) return;
		playerAnimator.SetInteger("Action", (int)animationType);
		SetAnimationSpeed(animationType);
	}
	public void AttackStart()
	{
		var monsterPool = GameMng.Ins.monsterPool.monsterList;
        for (int i = 0; i < monsterPool.Count; ++i)
		{
			if (monsterPool[i].active == false) continue;

			if (att.BaseAttack(playerMonsterAutoAttackVec,
				(monsterPool[i].gameObject.transform.position + new Vector3(0, monsterPool[i].monsterData.size, 0)) - (transform.position + new Vector3(0,0.3f,0)),
				calStat.attackRange + 0.3f,
				calStat.attackAngle))
			{
				playerStateMachine.attackDelayTime = 0;
				ChangeAnimation(ePlayerAnimation.Attack);
				playerStateMachine.isAttack = true;
			}
		}
	}
	public void Attack()
	{
		//애니메이션 이벤트 호출
		var monsterPool = GameMng.Ins.monsterPool.monsterList;
		for (int i = 0; i < monsterPool.Count; ++i)
		{
			if (monsterPool[i].active == false) continue;

			if (att.BaseAttack(playerMonsterAutoAttackVec,
				(monsterPool[i].transform.position + new Vector3(0, monsterPool[i].monsterData.size, 0)) - (transform.position + new Vector3(0, 0.3f, 0)),
				calStat.attackRange + 0.3f,
				calStat.attackAngle))
			{
				if (Rand.Permile(calStat.knockback))
				{
					monsterPool[i].OutStateAdd(new ConditionData(eBuffType.NockBack, 4, 1, 2), 300);
				}
				if (Rand.Permile(calStat.criticalChance))
				{
					monsterPool[i].Damage(eAttackType.Physics, calStat.damage * calStat.criticalDamage);
				}
				else
				{
					monsterPool[i].Damage(eAttackType.Physics, calStat.damage);
				}
                GameMng.Ins.HitToEffect(eAttackType.Physics,
                    monsterPool[i].transform.position + new Vector3(0, monsterPool[i].monsterData.size, 0), 
                    transform.position + new Vector3(0, calStat.size, 0));
            }
		}
		playerStateMachine.ChangeStateIdle();
	}

	private void AttackCheck()
	{
		int count = 0;
		var monsterPool = GameMng.Ins.monsterPool.monsterList;
        for (int i = 0; i < monsterPool.Count; ++i)
		{
			if (monsterPool[i].active == false) continue;


			//TODO : 몬스터 사이즈에 따라 업벡터 수정
			if (att.BaseAttack(playerMonsterAutoAttackVec,
				(monsterPool[i].gameObject.transform.position + new Vector3(0, monsterPool[i].monsterData.size, 0)) - (transform.position + new Vector3(0, 0.3f, 0)),
				calStat.attackRange + 0.3f,
				calStat.attackAngle))
			{
				count++;
			}
		}
		if(count == 0)
		{
			playerStateMachine.ChangeStateIdle();
		}
	}
	public void SetAnimationSpeed(ePlayerAnimation animationType)
	{
		switch(animationType)
		{
			case ePlayerAnimation.Attack:
				playerAnimator.speed = calStat.attackSpeed * Define.standardAttackSpeed;
				break;
			case ePlayerAnimation.Run:
				playerAnimator.speed = calStat.moveSpeed * Define.standardMoveSpeed;
				break;
			default:
				playerAnimator.speed = 1;
				break;
		}
	}
	public float GetTime(int skillIndex)
	{
		return conditionList[conditionList.FindID(skillIndex)].currentTime;
	}
	public Vector3 GetForward()
	{
		return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0);
	}
	public float GetFullHP()
	{
		return skillStat.healthPoint + calStat.GetHealthPoint(calStat.level);
	}
	public float GetEXPFill()
	{
		return (float)exp / JsonMng.Ins.expDataTable[calStat.level + 1].requiredExp;
	}
}
