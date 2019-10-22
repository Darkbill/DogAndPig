using System.Collections;
using UnityEngine;
public class BossMonster : Monster
{
    public SkillBurningMeteor BossSkill01;
    public override void Attack()
    {
        base.Attack();
        StartCoroutine(AttackAnimationDelay());
    }
    IEnumerator AttackAnimationDelay()
    {
        Debug.Log("몬스터 공격");
        yield return new WaitForSeconds(1);
        monsterStateMachine.ChangeStateMove();
    }
    public override void Dead()
    {
        base.Dead();
		GameMng.Ins.AllClear();
        //monsterStateMachine.ChangeStateDead();
    }
	public override void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
		UIMngInGame.Ins.bossHealthGageImage.fillAmount = GameMng.Ins.monsterPool.GetBossFill();
	}
}
