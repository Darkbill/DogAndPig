using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppeteerMonsterStateMachine : StateMachine
{
    public PuppeteerMonster monster;
    private float skillJumpDelayTime = 4.0f;
    private float skillJumpCoolTime = 0.0f;
    //TODO : Hp가 60% 아래로 떨어질 시 실행, 일단 테스트로 초기쿨타임ㅎ
    private float skillAttackDelayTime = 0.0f;
    private float skillAttackCoolTime = 0.0f;

    //몬스터 생성 시 최대 HP정보를 저장하기 위함
    private float firstHealthPoint;
    public override void Setting()
    {
        monster.jumpOffEffect = Instantiate(Resources.Load(string.Format("Skill/MonsterSKill/Skill_MonsterJumpDust"),
            typeof(ParticleSystem)) as ParticleSystem);
        monster.jumpOffEffect.Stop();

        monster.skill01 = Instantiate(Resources.Load(string.Format("Skill/MonsterSKill/Skill_TripleAttack"),
            typeof(SkillTripleAttack)) as SkillTripleAttack);

        stateDict.Add(eMonsterState.Idle, new MonsterStateIdle(monster));
        stateDict.Add(eMonsterState.Move, new PuppeteerMove(monster));
        stateDict.Add(eMonsterState.SkillAttack, new PuppeteerSkillAttack(monster));
        stateDict.Add(eMonsterState.SkillAttack2, new PuppeteerSkillJump(monster));
        stateDict.Add(eMonsterState.Attack, new MonsterStateAttack(monster));
        stateDict.Add(eMonsterState.Dead, new MonsterStateDead(monster));
        stateDict.Add(eMonsterState.Stun, new MonsterStateStun(monster));
        stateDict.Add(eMonsterState.KnockBack, new MonsterStateKnockBack(monster));
        cState = stateDict[eMonsterState.Idle];
        firstHealthPoint = monster.monsterData.healthPoint;
        cState.OnStart();
    }

    private void FixedUpdate()
    {
        skillJumpCoolTime += Time.deltaTime;
        skillAttackCoolTime += Time.deltaTime;
        cState.Tick();
        if (!SkillJumpAttackCheck())
        {
            ChangeState(eMonsterState.SkillAttack2);
            skillJumpCoolTime = 0;
            skillJumpDelayTime = Rand.Range(5, 7);
            return;
        }
        else if (!SkillAttackCheck())
        {
            ChangeState(eMonsterState.SkillAttack);
            skillAttackCoolTime = 0;
            skillAttackDelayTime = Rand.Range(5, 8);
            return;
        }

        if (cState.GetType() == typeof(MonsterStateIdle))
        {
            UpdateState();
        }
    }

    public override void UpdateState()
    {
        ChangeStateMove();
    }
    //애니메이션 호출 함수
    public void ChangeStateEndAttack()
    {
        ChangeStateIdle();
    }

    private bool SkillJumpAttackCheck()
    {
        if (skillJumpDelayTime >= skillJumpCoolTime)
            return true;
        return false;
    }
    private bool SkillAttackCheck()
    {
        if (skillAttackDelayTime >= skillAttackCoolTime ||
            (float)monster.monsterData.healthPoint / (float)firstHealthPoint > 0.6f)
            return true;
        return false;
    }
}
