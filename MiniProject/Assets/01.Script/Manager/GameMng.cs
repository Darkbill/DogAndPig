using UnityEngine;
using GlobalDefine;
using System.Collections.Generic;
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
	public List<int> hitMonsterIndex = new List<int>();
	public SkillMng skillMng;
	public int stageLevel;
	private void Awake()
	{
		//테스트 코드
		Time.timeScale = 1;
		stageLevel = 1;
        StartGame();
    }
	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			player.Damage(eAttackType.Physics, 10);
		}
	}
	public void StartGame()
    {
        monsterPool.StartStage(stageLevel);
    }
    public void StageClear()
    {
        stageLevel++;
        StartGame();
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