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
    public MilliMonster monster;
	public BulletPool bulletPool;
	public CameraMove cameraMove;
	//TODO : Event는 옵저버패턴 일괄처리
	public void DamageToPlayer(int damage)
	{
		cameraMove.OnStart();
		player.Damage(damage);
		UIMngInGame.Ins.DamageToPlayer(damage);
	}
}
	