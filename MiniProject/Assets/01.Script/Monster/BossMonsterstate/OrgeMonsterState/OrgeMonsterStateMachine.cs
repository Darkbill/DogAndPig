using UnityEngine;
using GlobalDefine;
public class OrgeMonsterStateMachine : StateMachine
{
	public OrgeMonster monster;
	private const float skillCoolTime = 6f;
	private const float skillCoolTime2 = 5f;
	private float skillDelayTime = 4f;
	private float skillDelayTime2 = 1f;
	public override void Setting()
	{
		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
		stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
		stateDict.Add(eMonsterState.Attack, new OrgeStateSwordDance(monster));
		stateDict.Add(eMonsterState.SkillAttack2, new OrgeStateSwordDance(monster));
		stateDict.Add(eMonsterState.SkillAttack, new OrgeSkillAttack(monster));
		cState = stateDict[eMonsterState.Idle];
		cState.OnStart();
	}
	private void FixedUpdate()
	{
		skillDelayTime += Time.deltaTime;
		skillDelayTime2 += Time.deltaTime;
		cState.Tick();
		if (SKillDelayCheck())
		{
			ChangeState(eMonsterState.SkillAttack);
			skillDelayTime = 0;
			return;
		}
		if (SkillDelayCheck2())
		{
			if(Rand.Percent(50))
			{
				ChangeStateAttack();
			}
			else
			{
				ChangeState(eMonsterState.SkillAttack2);
			}
		}
		if (cState.GetType() == typeof(MonsterStateIdle))
		{
			UpdateState();
		}
	}
	public override void UpdateState()
	{
		if (monster.AttackDistanceCheck())
		{
			if (monster.AttackDelayCheck() && monster.AttackCheck())
			{
				return;
			}
		}
		else
		{
			ChangeStateMove();
		}
	}
	private bool SKillDelayCheck()
	{
		if (skillDelayTime >= skillCoolTime)
		{
			skillDelayTime -= skillCoolTime;
			return true;
		}
		return false;
	}
	private bool SkillDelayCheck2()
	{
		if (skillDelayTime2 >= skillCoolTime2)
		{
			skillDelayTime2 -= skillCoolTime2;
			return true;
		}
		return false;
	}
}
