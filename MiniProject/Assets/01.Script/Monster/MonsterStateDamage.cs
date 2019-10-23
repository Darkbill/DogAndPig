using GlobalDefine;
public class MonsterStateDamage : MonsterState
{
	public MonsterStateDamage(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Damage);
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
