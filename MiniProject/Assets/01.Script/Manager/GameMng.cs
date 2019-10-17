using UnityEngine;
using GlobalDefine;
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
	public BulletPool bulletPool;
	public CameraMove cameraMove;
	public MonsterPool monsterPool;
	public SkillMng skillMng;
	private void Awake()
	{
		//테스트 코드
		Time.timeScale = 1;
	}
	public void DamageToPlayer(eAttackType attackType, float damage)
	{
		cameraMove.OnStart();
		player.Damage(attackType, damage);
	}
	public void GameOver()
	{
		//TODO : 옵저버
		cameraMove.GameOver();
		Time.timeScale = 0;
		UIMngInGame.Ins.GameOver();
	}
	public bool ActiveSkill(int skillID)
	{
		skillMng.skillDict[skillID].ActiveSkill();
		return true;
	}
}