using GlobalDefine;
using UnityEngine;
public class BossMonster : Monster
{
    public SkillBurningMeteor BossSkill01;
    public override void Dead()
    {
		monsterStateMachine.ChangeStateDead();
		active = false;
        ColliderOnOff(false);
		//GameMng.Ins.AddGold(JsonMng.Ins.playerInfoDataTable.stageLevel);
		//GameMng.Ins.AddEXP(JsonMng.Ins.playerInfoDataTable.stageLevel);
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
        transform.position = new ConfirmationArea().TargetSetting(transform.position);
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
