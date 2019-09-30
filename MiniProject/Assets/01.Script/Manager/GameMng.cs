using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
	#region SINGLETON
	static GameMng _instance = null;

	public static GameMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(GameMng)) as GameMng;
				if (_instance == null)
				{
					_instance = new GameObject("GameMng", typeof(GameMng)).GetComponent<GameMng>();
				}
			}

			return _instance;
		}
	}
	#endregion
	public Player player;
    public MonsterStateMachine monster;
	public BulletPool bulletPool;
    public BulletPool bulletMonster;
	public CameraMove cameraMove;
	public void DamagePlayer()
	{
		//TODO : 플레이어 HP 감소 등등 추가
		cameraMove.OnStart();
	}
}
	