using GlobalDefine;
using UnityEngine;

public class BossMonsterStateMachine : StateMachine
{
	public BossMonster monster;
	public override void Setting()
    {
        monster.BossSkill01 = Instantiate(Resources.Load(string.Format("Skill_FireBullet"), 
            typeof(SkillBurningMeteor)) as SkillBurningMeteor);
        stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
        stateDict.Add(eMonsterState.Move, new MonsterStateMove(monster));
        stateDict.Add(eMonsterState.SkillAttack, new BossMonsterStateSkillAttack(monster));
        stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
        stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
        stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
        stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
        cState = stateDict[eMonsterState.Idle];
        cState.OnStart();
    }
	public override void UpdateState()
	{
		if (monster.AttackDelayCheck())
		{
			ChangeStateAttack();
			return;
		}
	}
	public override void ChangeStateStun()
	{
		return;
	}
	public override void ChangeStateKnockBack(Vector3 _knockBackDir, float _knockBackPower)
	{
		return;
	}

    public void ChangeStateSkillAttack()
    {
        ChangeState(eMonsterState.SkillAttack);
    }
}
