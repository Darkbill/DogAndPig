using UnityEngine;
public class BossMonster : Monster
{
    public SkillBurningMeteor BossSkill01;
    public override void Dead()
    {
        base.Dead();
        BossSkill01.SkillButtonOff();
        GameMng.Ins.AllClear();
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
}
