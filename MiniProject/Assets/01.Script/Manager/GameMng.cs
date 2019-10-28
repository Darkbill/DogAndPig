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
	[HideInInspector]
	public int stageLevel;
	[HideInInspector]
	public int aimSkillID;
	private void Awake()
	{
		//테스트 코드
		Time.timeScale = 1;
		stageLevel = 1;
		player.PlayerSetting();
		aimSkillID = -1;
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
		UIMngInGame.Ins.StageClear();
    }
	public void AllClear()
	{
		monsterPool.AllClear();
		UIMngInGame.Ins.AllClear();
		//TODO : 
	}
	public void DamageToPlayer(eAttackType attackType, float damage)
	{
		player.Damage(attackType, damage);
    }
    public void HitToEffect(eAttackType type, Vector3 target, Vector3 pos)
    {
        //TODO : target - 맞는사람, pos - 공격자
        //GameMng.Ins.HitToEffect(eAttackType.Physics, GameMng.Ins.player.transform.position + new Vector3(0, 0.3f, 0),transform.position + new Vector3(0, 0.3f, 0));
        objectPool.effectPool.RunHitAnimation(type, target, pos);
    }
	public void GameOver()
	{
		//TODO : 옵저버
		cameraMove.GameOver();
	}
	public bool ActiveSkill(int skillID)
	{
		if (skillMng.skillDict[skillID].ActiveSkill()) return true;
		else return false;
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
	public void SetSkillAim(int skillID)
	{
		//에임이 필요한 스킬 발동시 호출
		if(aimSkillID != -1) OffSkillAim();
		aimSkillID = skillID;
		inputSystem.isSkillDrag = true;
	}
	public void StartSkillAim()
	{
		//에임 필요한 스킬 발동 후 드래그 시작시 호출
		player.isAim = true;
	}
	public void EndSkillAim()
	{
		//에임 필요한 스킬 발동 후 드래그 종료시 호출 //컴퓨터 빌드시 AimState에서, 모바일 빌드시 터치End일 때 호출
		ActiveSkill(aimSkillID);
		player.isAim = false;
		UIMngInGame.Ins.OnSkillDrop();
		aimSkillID = -1;
	}
	public void OffSkillAim()
	{
		//에임 필요한 스킬 재발동시 호출, 스킬사용 종료
		skillMng.skillDict[aimSkillID].OffAim();
		aimSkillID = -1;
		inputSystem.isSkillDrag = false;
		player.isAim = false;
		UIMngInGame.Ins.OffSkillAim();
	}
}