﻿using GlobalDefine;

public class BossMonsterStateSkillAttack : MonsterStateBase
{
    public BossMonsterStateSkillAttack(BossMonster o) : base(o)
    {

    }
    public override void OnStart()
    {
        monsterObject.ChangeAnimation(eMonsterAnimation.Skill);
        monsterObject.ColliderOnOff(false);
    }

    public override bool OnTransition()
    {
        return true;
    }

    public override void Tick()
    {
        if (OnTransition() == true) return;
    }
    public override void OnEnd()
    {

    }
}
