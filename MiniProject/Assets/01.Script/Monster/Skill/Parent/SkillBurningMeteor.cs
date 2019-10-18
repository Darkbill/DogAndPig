using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBurningMeteor : Skill
{
    #region SkillSetting
    enum eDashOption
    {
        Damage,
    }

    private const float rSiz = 3;

    eAttackType attackType = eAttackType.Fire;
    private float damage;
    public override void SkillSetting()
    {
        skillID = 6;
        PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
        skillType = skillData.skillType;
        target = skillData.target;
        damage = skillData.optionArr[(int)eDashOption.Damage];
        delayTime = cooldownTime;
    }
    #endregion

    public Targetting Target;

    private const int Count = 20;
    public List<Targetting> alterList = new List<Targetting>();


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
            //alterList[i].Setting(GameMng.Ins.player.transform.position, GameMng.Ins.player.transform.right, i + 5);
        }
        AttackSet = true;
    }

    private void FinishAttack()
    {

    }

    private void Update()
    {

    }
}
