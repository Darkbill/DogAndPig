using GlobalDefine;
public class RecordMonsterStateMachine : StateMachine
{
	public RecordMonster monster;
	public override void Setting()
	{
		stateDict.Add(eMonsterState.Idle, new RecordMonsterStateIdle(monster));
		stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
		stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
		stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
		stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
		stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
		cState = stateDict[eMonsterState.Idle];
		cState.OnStart();
	}
	public override void UpdateState()
	{

	}
}
