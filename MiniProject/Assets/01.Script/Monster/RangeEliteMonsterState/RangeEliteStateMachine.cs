using GlobalDefine;
using UnityEngine;
public class RangeEliteStateMachine : StateMachine
{
	public RangeEliteMonster monster;
	private const float skillCoolTime = 5f;
	private float skillDelayTime = 5f;
	public override void Setting()
	{
		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
		stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
		stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
		stateDict.Add(eMonsterState.SkillAttack, new RangeEliteSkillAttack(monster));
		cState = stateDict[eMonsterState.Idle];
		cState.OnStart();
	}
	private void FixedUpdate()
	{
		skillDelayTime += Time.deltaTime;
		cState.Tick();
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
				if (SKillDelayCheck())
				{
					monster.skillFlag = true;
					ChangeState(eMonsterState.SkillAttack);
				}
				else ChangeStateAttack();
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
		if(skillDelayTime >= skillCoolTime)
		{
			skillDelayTime -= skillCoolTime;
			return true;
		}
		return false;
	}
}
