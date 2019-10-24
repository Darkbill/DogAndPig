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
	public CameraMove cameraMove;
	public MonsterPool monsterPool;
	public SkillMng skillMng;
    public EffectPool effectPool;
	public int stageLevel;
	private void Awake()
	{
		//테스트 코드
		Time.timeScale = 1;
		stageLevel = 7;
		player.PlayerSetting();
        StartGame();
    }
	public void StartGame()
    {
        monsterPool.StartStage(stageLevel);
		UIMngInGame.Ins.RenewPlayerInfo();
    }
    public void StageClear()
    {
        stageLevel++;
        StartGame();
    }
	public void AllClear()
	{
		monsterPool.AllClear();
		UIMngInGame.Ins.AllClear();
		//TODO : 
	}
	public void DamageToPlayer(eAttackType attackType, float damage)
	{
		cameraMove.OnStart();
		player.Damage(attackType, damage);
    }
    public void HitToEffect(eAttackType type, Vector3 target, Vector3 pos)
    {
        //TODO : target - 맞는사람, pos - 공격자
        //GameMng.Ins.HitToEffect(eAttackType.Physics, GameMng.Ins.player.transform.position + new Vector3(0, 0.3f, 0),transform.position + new Vector3(0, 0.3f, 0));
        effectPool.RunHitAnimation(type, target, pos);
    }
	public void GameOver()
	{
		//TODO : 옵저버
		cameraMove.GameOver();
	}
	public bool ActiveSkill(int skillID)
	{
		skillMng.skillDict[skillID].ActiveSkill();
		return true;
	}
	public void AddGold(int gold)
	{
		JsonMng.Ins.playerInfoDataTable.AddGold(gold);
		UIMngInGame.Ins.AddGold(gold);
	}
	public void AddEXP(int exp)
	{
		player.AddEXP(exp);
		UIMngInGame.Ins.AddEXP();
	}
}