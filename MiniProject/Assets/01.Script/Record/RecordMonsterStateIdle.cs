using GlobalDefine;
using UnityEngine;
public class RecordMonsterStateIdle : MonsterStateBase
{
	public RecordMonsterStateIdle(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
	}

	public override bool OnTransition()
	{
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;

	}
	public override void OnEnd()
	{

	}
}
