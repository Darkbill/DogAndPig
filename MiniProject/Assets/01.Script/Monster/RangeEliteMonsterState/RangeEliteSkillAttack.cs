using GlobalDefine;
public class RangeEliteSkillAttack : MonsterStateBase
{
	public RangeEliteSkillAttack(RangeEliteMonster o) : base(o)
	{
		
	}
	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Skill);
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
		monsterObject.delayTime = 0;
	}
}
