using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : Skill
{
	enum eDashOption
	{
		Damage,
		coolTime,
	}
	private float damage;
	private float coolTime;
	public override void SkillSetting()
	{
		skillID = 1;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eDashOption.Damage];
		damage = skillData.optionArr[(int)eDashOption.Damage];
	}


	public Alter Bullet;

    private const int Count = 10;
    private List<Alter> alterList = new List<Alter>();


	public override void ActiveSkill()
	{
		gameObject.SetActive(true);
		GameMng.Ins.player.playerStateMachine.ChangeState(GlobalDefine.ePlayerState.Dash);
		GameMng.Ins.player.playerStateMachine.cState.isDash = true;
		for (int i = 0; i < Count; ++i)
		{
			GameObject o = Instantiate(Bullet.gameObject, gameObject.transform);
			alterList.Add(o.GetComponent<Alter>());
			alterList[i].Setting(GameMng.Ins.player.transform.position,GameMng.Ins.player.transform.right,i+5);
		}
	}
}
