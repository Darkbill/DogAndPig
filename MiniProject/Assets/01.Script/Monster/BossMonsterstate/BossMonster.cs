﻿using UnityEngine;
public class BossMonster : Monster
{
    public SkillBurningMeteor BossSkill01;
    public override void Dead()
    {
		monsterStateMachine.ChangeStateDead();
		active = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		GameMng.Ins.AddGold(GameMng.Ins.stageLevel);
		GameMng.Ins.AddEXP(GameMng.Ins.stageLevel);
		BossSkill01.SkillButtonOff();
    }
	public override void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.ShowDamage(d, Camera.main.WorldToScreenPoint(gameObject.transform.position));
		if (monsterData.healthPoint <= 0) Dead();
		UIMngInGame.Ins.bossHealthGageImage.fillAmount = GameMng.Ins.monsterPool.GetBossFill();
	}
    public void AttackStart()
    {
        BossSkill01.SkillButtonOn();
    }
	public void AllClear()
	{
		GameMng.Ins.AllClear();
	}
}
