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
	public ObjectPool objectPool;
	public InputSystem inputSystem;
	public Portal portal;
	//테스트코드
	public static int stageLevel;
	public static int worldLevel;
	[Range(0, 10700)]
	public int exp;
	[HideInInspector]
	public int aimSkillID;
	private void Awake()
	{
		LoadGame();
	}
	public void LoadGame()
	{
		//테스트 코드
		Time.timeScale = 1;
		player.PlayerSetting();
		aimSkillID = -1;
		StartGame();
		//if (JsonMng.Ins.playerInfoDataTable.StartGame())
		//{
		//	Debug.Log("광고");
		//}
	}
	public void ChangeStage()
	{
		JsonMng.Ins.playerInfoDataTable.playerLevel = 1;
		JsonMng.Ins.playerInfoDataTable.exp = 0;
		AddEXP(exp);
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}
	public void StartGame()
	{
		UIMngInGame.Ins.StageStart();
		monsterPool.StartStage(worldLevel, stageLevel);
	}
	public void StageClear()
	{
        objectPool.goodmng.AllRunningSelect();
        stageLevel++;
        portal.NextStagePotal();
	}
	public void WorldClear()
	{
		Time.timeScale = 0;
		JsonMng.Ins.playerInfoDataTable.clearLevel++;
		worldLevel++;
		stageLevel = 1;
		UIMngInGame.Ins.AllClear();
		//TODO : 클리어보상
	}
	public void MonsterDead(int gold,int exp)
	{
		monsterPool.DeadMonster();
	}
	public void DamageToPlayer(eAttackType attackType, float damage)
	{
		player.Damage(attackType, damage);
	}
	public void HitToEffect(eAttackType type, Vector3 target, Vector3 pos, float siz)
	{
		//TODO : target - 맞는사람, pos - 공격자
		//GameMng.Ins.HitToEffect(eAttackType.Physics, GameMng.Ins.player.transform.position + new Vector3(0, 0.3f, 0),transform.position + new Vector3(0, 0.3f, 0));
		objectPool.effectPool.RunHitAnimation(type, target, pos, siz);
	}
	public void GameOver()
	{
		skillMng.OffSkill();
		cameraMove.GameOver();
	}
	public void AddGold(int gold)
	{
		JsonMng.Ins.playerInfoDataTable.AddGold(gold);
		UIMngInGame.Ins.AddGold(gold);
	}
	public void AddDiamond(int dia)
	{
		JsonMng.Ins.playerInfoDataTable.AddGold(dia);
		UIMngInGame.Ins.AddDiamond(dia);
	}
	public void AddEXP(int exp)
	{
		player.AddEXP(exp);
		UIMngInGame.Ins.AddEXP();
	}

	public void ActiveSkill(int skillID)
	{
		skillMng.skillDict[skillID].OnButtonDown();
	}
	public void SetSkillAim(int skillID)
	{
		//에임이 필요한 스킬 발동시 호출
		if (aimSkillID != -1) OffSkillAim();
		aimSkillID = skillID;
		inputSystem.isSkillDrag = true;
		UIMngInGame.Ins.HightLightSkillSet(true);
	}
	public void StartSkillAim()
	{
		//에임 필요한 스킬 발동 후 드래그 시작시 호출
		skillMng.skillDict[aimSkillID].OnDrag();
		player.isAim = true;
	}
	public void EndSkillAim()
	{
		//에임 필요한 스킬 발동 후 드래그 종료시 호출 //컴퓨터 빌드시 AimState에서, 모바일 빌드시 터치End일 때 호출
		//ActiveSkill(aimSkillID);
		skillMng.skillDict[aimSkillID].OnDrop();
		player.isAim = false;
		UIMngInGame.Ins.OnSkillDrop();
		aimSkillID = -1;
	}
	public void OffSkillAim()
	{
		//에임 필요한 스킬 재발동시 호출, 스킬사용 종료
		inputSystem.isSkillDrag = false;
		if(skillMng.skillDict[aimSkillID].activeFlag)
		{
			skillMng.skillDict[aimSkillID].OnDrop();
		}
		aimSkillID = -1;
		player.isAim = false;
		UIMngInGame.Ins.OffSkillAim();
	}
}