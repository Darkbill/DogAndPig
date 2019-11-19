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
	public HPUIMng hpUIMng;
	//테스트코드
	public bool isRecord;
	public static int stageLevel;
	public static int worldLevel;
	[Range(0, 10700)]
	public int exp;
	[HideInInspector]
	public int aimSkillID;
	private void Start()
	{
		if (isRecord && JsonMng.Ins.IsDone)
		{
			StartToRecord();
		}
		else LoadGame();
	}
	public void StartToRecord()
	{
		skillMng.LoadAll();
		player.PlayerSetting();
		worldLevel = 0;
		aimSkillID = -1;
		WorldStartToRecord();
	}
	public void WorldStartToRecord()
	{
		monsterPool.WorldStart(worldLevel);
		stageLevel = 1;
	}
	public void LoadGame()
	{
		//테스트 코드
		Time.timeScale = 1;
		aimSkillID = -1;
		player.PlayerSetting();
		WorldStart();
		if (JsonMng.Ins.playerInfoDataTable.StartGame())
		{
			Debug.Log("광고");
		}
	}
	public void ChangeStage()
	{
		JsonMng.Ins.playerInfoDataTable.playerLevel = 1;
		JsonMng.Ins.playerInfoDataTable.exp = 0;
		AddExpToValue(exp);
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}
	public void StageClear()
	{
        objectPool.goodmng.AllRunningSelect();
        portal.NextStagePotal();
		AddExp(false);
	}
	public void StartStage()
	{
		stageLevel++;
		skillMng.OffSkill();
		OffSkillAim();
		monsterPool.StartStage(stageLevel);
		UIMngInGame.Ins.StageStart();
	}
	public void ContinueStage()
	{
		//TODO : 플레이어 부활
	}
	public void WorldStart()
	{
		UIMngInGame.Ins.StageStart();
		monsterPool.WorldStart(worldLevel);
		stageLevel = 1;
	}
	public void WorldClear()
	{
		Time.timeScale = 0;
		if(worldLevel == JsonMng.Ins.playerInfoDataTable.clearLevel + 1) JsonMng.Ins.playerInfoDataTable.clearLevel++;
		UIMngInGame.Ins.AllClear();
	}
	public void MonsterDead()
	{
		monsterPool.DeadMonster();
	}
	public void DamageToPlayer(eAttackType attackType, float damage)
	{
		player.Damage(attackType, damage);
	}
	public void HitToEffect(eAttackType type, Vector3 target, Vector3 pos, float siz)
	{
		objectPool.effectPool.RunHitAnimation(type, target, pos, siz);
	}
	public void GameOver()
	{
		cameraMove.GameOver();
		skillMng.OffSkill();
	}
	public void AddGold(int gold)
	{
		JsonMng.Ins.playerInfoDataTable.AddGold(gold);
		UIMngInGame.Ins.AddGold(gold);
	}
	public void AddDiamond(int dia)
	{
		JsonMng.Ins.playerInfoDataTable.AddDiamond(dia);
		UIMngInGame.Ins.AddDiamond(dia);
	}
	public void AddExpToValue(int exp)
	{
		//테스트용 함수
		player.AddEXP(exp);
		UIMngInGame.Ins.AddEXP();
	}
	public void AddExp(bool isBoss)
	{
		if(isBoss) player.AddEXP(worldLevel * worldLevel * stageLevel * 3);
		else player.AddEXP(worldLevel * worldLevel * stageLevel);
		UIMngInGame.Ins.AddEXP();
	}
	public void ActiveSkill(int skillID)
	{
		skillMng.skillDict[skillID].OnButtonDown();
	}
	public void SetSkillAim(int skillID)
	{
		//에임이 필요한 스킬 발동시 호출
		OffSkillAim();
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
		skillMng.skillDict[aimSkillID].OnDrop();
		player.isAim = false;
		UIMngInGame.Ins.OnSkillDrop();
		aimSkillID = -1;
	}
	public void EndSkillAim(Vector2 pos)
	{
		//모바일빌드 시 마우스위치가 아닌 터치위치 기반으로 스킬 발동
		skillMng.skillDict[aimSkillID].OnDrop(pos);
		player.isAim = false;
		UIMngInGame.Ins.OnSkillDrop();
		aimSkillID = -1;
	}
	public void OffSkillAim()
	{
		//에임 필요한 스킬 재발동시 호출, 스킬사용 종료
		if (aimSkillID == -1) return;
		inputSystem.isSkillDrag = false;
		if(skillMng.skillDict[aimSkillID].activeFlag)
		{
			skillMng.skillDict[aimSkillID].OnDrop();
		}
		aimSkillID = -1;
		player.isAim = false;
		UIMngInGame.Ins.OffSkillAim();
	}
	public void SetHPMonster(Monster monster)
	{
		hpUIMng.Setting(monster);
	}
}