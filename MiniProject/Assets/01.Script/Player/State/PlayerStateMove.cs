using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class PlayerStateMove : PlayerState
{
	public float fHorizontal { get; set; }
	public float fVertical { get; set; }
	public bool isMove;

	public Movement Mov = new Movement();

	public PlayerStateMove(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
        
		Moving();
        Debug.Log(Mathf.Acos(Mathf.Cos(playerObject.transform.eulerAngles.z * Mathf.Deg2Rad)) * Mathf.Rad2Deg);

        Attack();
		if (fVertical == 0 && fHorizontal == 0)
			playerObject.playerStateMachine.ChangeState(ePlayerState.Idle);
		return true;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
	}
	public override void OnEnd()
	{
        
	}
    private void Attack()
	{
		//TODO : 반경있으면 공격하기로 
		ObjectSetAttack att = new ObjectSetAttack();
		var monsterPool = GameMng.Ins.monsterPool.monsterList;
		for(int i = 0; i < monsterPool.Count; ++i)
		{
			if (att.BaseAttack(playerObject.transform.right,
			   monsterPool[i].gameObject.transform.position - playerObject.transform.position,
			   playerObject.calStat.attackRange,
			   playerObject.calStat.attackAngle
			   ))
			{
				if (Rand.Percent(playerObject.calStat.criticalChange))
				{
					monsterPool[i].Damage(eAttackType.Physics, playerObject.calStat.damage * playerObject.calStat.criticalDamage);
				}
				else
				{
					monsterPool[i].Damage(eAttackType.Physics, playerObject.calStat.damage);
				}
			}
		}

	}

	private void Moving()
	{
		fHorizontal = Input.GetAxis("Horizontal");
		fVertical = Input.GetAxis("Vertical");

		Mov.iSpeed = playerObject.calStat.moveSpeed;
		playerObject.transform.position += Mov.Move(fHorizontal, fVertical);

		float Degree = Mathf.Atan2(fVertical, fHorizontal) * Mathf.Rad2Deg;
		playerObject.transform.eulerAngles = new Vector3(0, 0, Degree);
		
	}
}
