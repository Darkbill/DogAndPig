public class BossMonster : Monster
{
    public SkillBurningMeteor BossSkill01;
    public override void Dead()
    {
		StateMachine.ChangeStateDead();
		active = false;
        ColliderOnOff(false);
        GameMng.Ins.objectPool.goodmng.RunningSelect(1, 10, gameObject.transform.position);
        GameMng.Ins.objectPool.goodmng.RunningSelect(2, 5, gameObject.transform.position);
		GameMng.Ins.AddExp(true);
        BossSkill01.SkillButtonOff();
    }
	public override void DamageResult(int d)
	{
		if (d < 1) d = 1;
		monsterData.healthPoint -= d;
		UIMngInGame.Ins.DamageToBoss(d, transform.position);
		if (monsterData.healthPoint <= 0) Dead();
	}
    public void ChangeRandomMove()
    {
        transform.position = new ConfirmationArea().RangeRandomResult(transform.position, 2.0f);
        ColliderOnOff(true);
    }
    public void AttackStart()
    {
        float playerStreetXpos = GameMng.Ins.player.transform.position.x - transform.position.x;
        if (playerStreetXpos < 0)
            Angle = 180;
        else
            Angle = 0;
        BossSkill01.SkillButtonOn();
    }
	public void AllClear()
	{
		GameMng.Ins.WorldClear();
	}
}
