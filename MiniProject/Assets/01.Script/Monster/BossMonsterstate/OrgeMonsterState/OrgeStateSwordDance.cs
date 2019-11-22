using GlobalDefine;
public class OrgeStateSwordDance : MonsterStateBase
{
	public OrgeStateSwordDance(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Skill2);
	}

	public override bool OnTransition()
	{
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
		ChangeDegree();
	}
	public void ChangeDegree()
	{

	}
	public override void OnEnd()
	{

	}
}
