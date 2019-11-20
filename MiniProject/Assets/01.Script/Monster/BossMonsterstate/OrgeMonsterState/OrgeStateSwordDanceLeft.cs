using GlobalDefine;
public class OrgeStateSwordDanceLeft : MonsterStateBase
{
	public OrgeStateSwordDanceLeft(Monster o) : base(o)
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
