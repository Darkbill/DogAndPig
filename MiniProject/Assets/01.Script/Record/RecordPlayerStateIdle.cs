using GlobalDefine;
public class RecordPlayerStateIdle : PlayerState
{
	public RecordPlayerStateIdle(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
		playerObject.ChangeAnimation(ePlayerAnimation.Idle);
	}

	public override bool OnTransition()
	{
		return false;
	}

	public override void Tick()
	{
	}
	public override void OnEnd()
	{

	}
}
