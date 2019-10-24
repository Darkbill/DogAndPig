using GlobalDefine;
using UnityEngine;

public class WizardMonsterStateMove : MonsterState
{
    Vector2 directionToPlayer;
    float degreeToPlayer;
    Vector3 ownerDirection = new Vector3(1, 0, 0);

    public WizardMonsterStateMove(WizardMonster o) : base(o)
    {
    }

    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
    }

    public override bool OnTransition()
    {
		if (monsterObject.monsterStateMachine.IsAttack() && monsterObject.AttackCheckStart())
		{
			//TODO : 모션추가
			monsterObject.monsterStateMachine.ChangeStateAttack();
			return true;
		}
		return false;
	}

    public override void Tick()
    {
        if (OnTransition() == true) return;
        ChaseToPlayer();
    }
    public override void OnEnd()
    {

    }
    public void ChaseToPlayer()
    {
		//TODO : 쫓아가는거말고 따른걸로
        //ownerDirection = monsterObject.GetForward();
        //directionToPlayer = GameMng.Ins.player.transform.position - monsterObject.gameObject.transform.position;
        //float ownerDegree = Mathf.Atan2(ownerDirection.y, ownerDirection.x);
        //float goalDegree = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
        //degreeToPlayer = (ownerDegree - goalDegree) * Mathf.Rad2Deg;

        //if (degreeToPlayer > 180) { degreeToPlayer -= 360; }
        //else if (degreeToPlayer < -180) { degreeToPlayer += 360; }

        //if (degreeToPlayer < 0)
        //    monsterObject.Angle +=
        //        Time.deltaTime * monsterObject.monsterData.rotationSpeed;
        //else
        //    monsterObject.Angle -=
        //        Time.deltaTime * monsterObject.monsterData.rotationSpeed;

        //monsterObject.gameObject.transform.position +=
        //    ownerDirection *
        //    Time.deltaTime *
        //    monsterObject.monsterData.moveSpeed;
        
    }
}
