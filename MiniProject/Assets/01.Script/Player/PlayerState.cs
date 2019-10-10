using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public PlayerStateMachine playerObject;

    public int level = 1;
    public int nowHealthPoint;
    public PlayerData playerData;

    public PlayerState(PlayerStateMachine o)
	{
		playerObject = o;
        PlayerSetting();
	}
    private void PlayerSetting()
    {
        playerData = JsonMng.Ins.playerDataTable[level];
        nowHealthPoint = playerData.healthPoint;
    }

    public abstract void OnStart();
	public abstract void Tick();
	public abstract void OnEnd();
	public abstract bool OnTransition();
}
