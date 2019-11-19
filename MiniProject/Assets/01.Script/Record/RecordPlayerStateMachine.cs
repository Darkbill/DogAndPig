using GlobalDefine;
public class RecordPlayerStateMachine : PlayerStateMachine
{
	private void Awake()
	{
		Setting();
		stateDict[ePlayerState.Idle] = new RecordPlayerStateIdle(gameObject.GetComponent<Player>());
		cState = stateDict[ePlayerState.Idle];
		cState.OnStart();
	}
}
