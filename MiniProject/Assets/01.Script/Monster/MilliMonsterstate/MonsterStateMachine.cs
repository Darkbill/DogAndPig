using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class MonsterStateMachine : MonoBehaviour
{
	public Dictionary<eMilliMonsterState, MonsterState> stateDict = new Dictionary<eMilliMonsterState, MonsterState>();
	private int monsterID = 1;
	public MonsterData monsterData;
	public MonsterState cState;
	private void Awake()
	{
		Setting();
	}
	private void Setting()
	{
		monsterData = JsonMng.Ins.monsterDataTable[monsterID];
		stateDict.Add(eMilliMonsterState.Idle, new MilliMonsterStateIdle(this));
		stateDict.Add(eMilliMonsterState.Move, new MilliMonsterStateMove(this));
		stateDict.Add(eMilliMonsterState.Stun, new MilliMonsterStateStun(this));
		stateDict.Add(eMilliMonsterState.SkillAttack, new MilliMonsterStateSkillAttack(this));
		stateDict.Add(eMilliMonsterState.Dash, new MilliMonsterStateDash(this));
		stateDict.Add(eMilliMonsterState.Dead, new MilliMonsterStateDead(this));
		cState = stateDict[eMilliMonsterState.Idle];
		cState.OnStart();
	}
	public void ChangeState(eMilliMonsterState stateType)
	{
		cState.OnEnd();
		cState = stateDict[stateType];
		cState.OnStart();
	}
	private void Update()
	{
		cState.Tick();
	}
	/* 테스트코드 */
	public void Attack()
	{
		StartCoroutine(AttackAnimationDelay());
	}
	IEnumerator AttackAnimationDelay()
	{
		Debug.Log("몬스터 공격");
		//TODO : 데미지 계산함수
		GameMng.Ins.DamageToPlayer(2);
		yield return new WaitForSeconds(1);
		ChangeState(eMilliMonsterState.Move);
	}
    public void Hit()
    {
        Debug.Log("몬스터가 공격을 당했습니다.");
    }
}
