using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDash : Skill
{
	#region SkillSetting
	enum eDashOption
	{
		Damage,
		coolTime,
	}

    eAttackType attackType = eAttackType.Wind;
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
    }
	#endregion

	public Alter Bullet;

    private const int Count = 10;
    public List<Alter> alterList = new List<Alter>();
    

    bool AttackSet = false;


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
        AttackSet = true;
    }

    private void FinishAttack()
    {
        List<Collider2D> Attack = new List<Collider2D>();

        for (int i = 0; i < Count; ++i)
            for(int j = 0;j<alterList[i].Attack.Count;++j)
                Attack.Add(alterList[i].Attack[j]);

        Attack = Attack.Distinct().ToList();

        for(int i = 0;i<Attack.Count;++i)
        {
            Attack[i].GetComponent<Monster>().Damage(attackType, damage);
        }
    }

	private void Update()
	{
		delayTime += Time.deltaTime;
        int counting = 0;
        for(int i = 0;i<Count;++i)
        {
            if (!alterList[i].gameObject.activeSelf)
                ++counting;
        }
        if(counting == Count - 1 && AttackSet)
        {
            FinishAttack();
            AttackSet = false;
        }
	}
}
