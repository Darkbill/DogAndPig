using GlobalDefine;
public class GhoulMonsterStateMachine : StateMachine
{
	public GhoulMonster monster;
	public override void Setting()
	{

		stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
		stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
		stateDict.Add(eMonsterState.SkillAttack, new GhoulMonsterStateSkillAttack(monster));
		stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
		cState = stateDict[eMonsterState.Idle];
		cState.OnStart();
	}
	public override void UpdateState()
	{
		if (monster.AttackDistanceCheck())
		{
			if (monster.AttackDelayCheck())
			{
				ChangeStateAttack();
				return;
			}
		}
		else
		{
			ChangeStateMove();
		}
	}
}
