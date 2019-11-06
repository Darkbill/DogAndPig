using GlobalDefine;
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
	private Monster attackMonster;
	public void PlayerSetting()
	{
		calStat = JsonMng.Ins.playerDataTable;
		CalculatorStat();
	}
	public void CalculatorStat()
	{
		calStat = JsonMng.Ins.playerDataTable.AddStat(skillStat, conditionList);
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
		if (playerStateMachine.isAttack)
		{
			Vector3 dir = attackMonster.transform.position - transform.position;
			degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		}
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
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
		ChangePlayerAngle();
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
		JsonMng.Ins.playerInfoDataTable.exp += _exp;
		if(JsonMng.Ins.playerInfoDataTable.exp >= JsonMng.Ins.expDataTable[GetLevel()+ 1].requiredExp)
		{
			int saveEXP = JsonMng.Ins.expDataTable[GetLevel() + 1].requiredExp - JsonMng.Ins.playerInfoDataTable.exp;
			LevelUP();
			AddEXP(Mathf.Abs(saveEXP));
		}
	}

	private void LevelUP()
	{
		JsonMng.Ins.playerInfoDataTable.playerLevel++;
		JsonMng.Ins.playerInfoDataTable.exp = 0;
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
		//Attack 쿨타임이 채워지면 공격가능 범위 내에 가장 가까운 적 등록, Attack시작
		var monsterPool = GameMng.Ins.monsterPool.monsterList;
		float distance = float.MaxValue;
		int monsterIndex = -1;
        for (int i = 0; i < monsterPool.Count; ++i)
		{
			if (monsterPool[i].active == false) continue;

			if (att.BaseAttack(GetForward(),
				(monsterPool[i].gameObject.transform.position + new Vector3(0, monsterPool[i].monsterData.size, 0)) - (transform.position + new Vector3(0,calStat.size,0)),
				calStat.attackRange + calStat.size,
				calStat.attackAngle))
			{
				float m = (monsterPool[i].gameObject.transform.position - gameObject.transform.position).magnitude;
				if (distance > m)
				{
					distance = m;
					monsterIndex = i;
				}
			}
		}
		if (monsterIndex == -1) return;
		AttackSet(true);
		attackMonster = GameMng.Ins.monsterPool.monsterList[monsterIndex];
	}
	private void AttackSet(bool isAttack)
	{
		if(isAttack)
		{
			playerStateMachine.attackDelayTime = 0;
			ChangeAnimation(ePlayerAnimation.Attack);
			playerStateMachine.isAttack = true;
		}
		else
		{
			attackMonster = null;
			playerStateMachine.ChangeStateIdle();
		}
	}
	public void Attack()
	{
		//애니메이션 이벤트 호출
		if(AttackCheck())
		{
			if (Rand.Permile(calStat.knockback))
			{
				attackMonster.OutStateAdd(new ConditionData(eBuffType.NockBack, 0, 0, 2));
			}
			if (Rand.Permile(calStat.criticalChance))
			{
				attackMonster.Damage(eAttackType.Physics, calStat.damage * calStat.criticalDamage);
			}
			else
			{
				attackMonster.Damage(eAttackType.Physics, calStat.damage);
			}
			GameMng.Ins.HitToEffect(eAttackType.Physics,
				attackMonster.transform.position + new Vector3(0, attackMonster.monsterData.size, 0),
				transform.position + new Vector3(0, calStat.size, 0),
                attackMonster.monsterData.size);
		}
		
		playerStateMachine.ChangeStateIdle();
	}

	private bool AttackCheck()
	{
		//Attack시작 부터 종료될 때까지 등록된 몬스터가 존재하는지 체크
		if (attackMonster == null || attackMonster.active == false)
		{
			AttackSet(false);
			return false;
		}
		if (att.BaseAttack(GetForward(),
			(attackMonster.gameObject.transform.position + new Vector3(0, attackMonster.monsterData.size, 0)) - (transform.position + new Vector3(0, calStat.size, 0)),
			calStat.attackRange + calStat.size,
			calStat.attackAngle)) return true;
		else
		{
			AttackSet(false);
			return false;
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
		return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0).normalized;
	}
	public float GetFullHP()
	{
		return skillStat.healthPoint + calStat.GetHealthPoint(GetLevel());
	}
	public float GetEXPFill()
	{
		return (float)JsonMng.Ins.playerInfoDataTable.exp / JsonMng.Ins.expDataTable[GetLevel() + 1].requiredExp;
	}
	public int GetLevel()
	{
		return JsonMng.Ins.playerInfoDataTable.playerLevel;
	}
}
