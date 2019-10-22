using UnityEngine;
public class MilliMonster : Monster
{
	public override void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
		else monsterStateMachine.ChangeStateDamage();
	}
}
