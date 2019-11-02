using GlobalDefine;
using UnityEngine;
public class MonsterStateIdle : MonsterState
{
	public MonsterStateIdle(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
	}

	public override bool OnTransition()
	{
		if (monsterObject.AttackCheckStart() == false)
		{
			monsterObject.monsterStateMachine.ChangeStateMove();
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;

		if (monsterObject.monsterStateMachine.IsAttack())
		{
			monsterObject.monsterStateMachine.ChangeStateAttack();
		}
		ChangeDegree();
	}
	public void ChangeDegree()
	{
		Vector3 ownerDirection = monsterObject.GetForward();
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - monsterObject.gameObject.transform.position;
		float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
		float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
		float degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

		if (degreeToPlayer > 180) { degreeToPlayer -= 360; }
		else if (degreeToPlayer < -180) { degreeToPlayer += 360; }

		if (degreeToPlayer < 0)
			monsterObject.Angle +=
				Time.deltaTime * monsterObject.monsterData.rotationSpeed;
		else
			monsterObject.Angle -=
				Time.deltaTime * monsterObject.monsterData.rotationSpeed;
	}
	public override void OnEnd()
	{

	}
}
