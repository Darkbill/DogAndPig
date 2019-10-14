using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : Skill
{
	#region SkillSetting
	enum eDashOption
	{
		Damage,
		coolTime,
	}
	private float damage;
	public override void SkillSetting()
	{
		skillID = 1;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eDashOption.Damage];
		cooldownTime = skillData.optionArr[(int)eDashOption.coolTime];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
        for (int i = 0; i < Count; ++i)
            alterList[i].damage = damage;
    }
	#endregion

	public Alter Bullet;

    private const int Count = 10;
    public List<Alter> alterList = new List<Alter>();


	public override void ActiveSkill()
	{
		base.ActiveSkill();
		//테스트 코드
		GameMng.Ins.player.AddBuff(new ConditionData(GlobalDefine.eBuffType.MoveFast, 1, 3, 2));
		GameMng.Ins.player.playerStateMachine.ChangeState(GlobalDefine.ePlayerState.Dash);
		GameMng.Ins.player.playerStateMachine.cState.isDash = true;
		for (int i = 0; i < Count; ++i)
		{
			alterList[i].Setting(GameMng.Ins.player.transform.position,GameMng.Ins.player.transform.right,i+5);
		}
	}

	private void Update()
	{
		delayTime += Time.deltaTime;
	}
}
