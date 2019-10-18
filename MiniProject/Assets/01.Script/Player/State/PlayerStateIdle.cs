using UnityEngine;
using GlobalDefine;
public class PlayerStateIdle : PlayerState
{
	public PlayerStateIdle(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
	}

	public override bool OnTransition()
	{
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Move);
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;

		delayTime += Time.deltaTime;
		if (delayTime >= playerObject.calStat.attackSpeed)
		{
			ObjectSetAttack att = new ObjectSetAttack();
			var monsterPool = GameMng.Ins.monsterPool.monsterList;
			for (int i = 0; i < monsterPool.Count; ++i)
			{
				if (monsterPool[i].gameObject.activeSelf == false) continue;
				if (att.BaseAttack(playerObject.transform.right,
					monsterPool[i].gameObject.transform.position - playerObject.transform.position,
					playerObject.calStat.attackRange,
					playerObject.calStat.attackAngle))
				{
					delayTime = 0;
					playerObject.ChangeAnimation(ePlayerAnimation.Attack);
					if (playerObject.calStat.knockbackAtt)
						monsterPool[i].OutStateAdd(new ConditionData(eBuffType.NockBack, 4, 1, 2), 300);
					if (Rand.Percent(playerObject.calStat.criticalChance))
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
	}
	public override void OnEnd()
	{

	}
}
