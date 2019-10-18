public abstract class PlayerState
{
	public Player playerObject;
	public bool isDash;
	public float delayTime;
    public PlayerState(Player o)
	{
		playerObject = o;
	}
	public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
