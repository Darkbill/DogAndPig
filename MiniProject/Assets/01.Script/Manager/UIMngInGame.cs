using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMngInGame : MonoBehaviour
{
    #region SINGLETON
    static UIMngInGame _instance = null;

    public static UIMngInGame Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(UIMngInGame)) as UIMngInGame;
                if (_instance == null)
                {
                    _instance = new GameObject("UIMngInGame", typeof(UIMngInGame)).GetComponent<UIMngInGame>();
                }
            }

            return _instance;
        }
    }
    #endregion
    /* UI List */
    public GameObject gameOverUI;
    public GameObject WaitUI;
	public GameObject ClearUI;
	public GameObject healthPack;
	public GameObject bossInfo;
	public BuffUI buffUI;
	public SkillUI skillUI;
	public PlayerInfoUI playerInfoUI;
	public DamageTextPool damageTextPool;
	public Aim aimImage;

	/* Image */
	public Image stickImage;
	public Image bossHealthGageImage;
	public Image fade;

	private Vector3 stickPos; //터치 눌러서 joyStick이 시작한 위치
	private float stickRadius = 60;
    private Vector3 dir;

	private void Start()
    {
        UISetting();
    }
	public void ActiveBuff(int skillIndex)
    {
        buffUI.ActiveBuff(skillIndex);
    }
    public void UISetting()
    {
		playerInfoUI.Setting();
		skillUI.Setting();
	}
	public void RenewPlayerInfo()
	{
		playerInfoUI.RenewPlayerInfo();
	}
	#region MoveUI
	public void OnStrickDrag()
    {
        GameMng.Ins.player.isMove = true;
		stickImage.gameObject.SetActive(true);
		stickImage.gameObject.transform.position = Input.mousePosition;
        stickPos = stickImage.gameObject.transform.position;
    }
    public void OnStickDrop()
    {
        GameMng.Ins.player.isMove = false;
		stickImage.gameObject.SetActive(false);
		aimImage.gameObject.SetActive(false);
    }
	public void OnSkillDrag(int touchID)
	{
		aimImage.SetTouchID(touchID);
		aimImage.gameObject.SetActive(true);
		GameMng.Ins.StartSkillAim();
	}
	public void OnSkillDrop()
	{
		aimImage.gameObject.SetActive(false);
		HightLightSkillSet(false);
		CoolDownAllSkill();
	}
	public void OffSkillAim()
	{
		aimImage.gameObject.SetActive(false);
		HightLightSkillSet(false);
	}
	public Vector3 GetJoyStickDirection()
	{
		/* 컴퓨터 빌드 */
#if UNITY_EDITOR_WIN
		stickImage.gameObject.transform.position = Input.mousePosition;
		dir = stickImage.gameObject.transform.position - stickPos;
		float m = dir.magnitude;
		dir.Normalize();
		if (m > stickRadius)
		{
			stickImage.gameObject.transform.position = stickPos + dir * stickRadius;
        }
		else
		{
			stickImage.gameObject.transform.position = Input.mousePosition;
		}
        return dir;
#else
		/* 모바일 빌드 */
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch tempTouchs = Input.GetTouch(i);

			if (tempTouchs.fingerId == GameMng.Ins.inputSystem.touchID)
			{
				stickImage.gameObject.transform.position = tempTouchs.position;
				dir = stickImage.gameObject.transform.position - stickPos;
				float m = dir.magnitude;
				dir.Normalize();
				if (m > stickRadius)
				{
					stickImage.gameObject.transform.position = stickPos + dir * stickRadius;
				}
				else
				{
					stickImage.gameObject.transform.position = tempTouchs.position;
				}
				return dir;
			}
		}
		return Vector3.zero;
#endif
	}
	#endregion
	/* Skill */
    public void StartSkillSet(int skillnum)
    {
		skillUI.StartSkillSet(skillnum);
    }

    public void HightLightSkillSet(bool onCheck)
    {
		skillUI.HightLightSkillSet(onCheck);
    }

    public void CoolDownAllSkill()
    {
		skillUI.CoolDownAllSkill();
    }

	/* Damage */
    public void DamageToPlayer(int damage)
    {
        damageTextPool.ShowDamage(damage, Camera.main.WorldToScreenPoint(GameMng.Ins.player.transform.position));
		playerInfoUI.DamageToPlayer(damage);
    }
	/* Button */
    public void GameOver()
    {
        WaitUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
    }
    public void InerruptGame(int time)
    {
        Time.timeScale = time;
    }
    public void InerruptGameSet()
    {
        WaitUI.gameObject.SetActive(true);
    }
    public void OnClickContinue()
    {
		Debug.Log("광고");
		GameMng.Ins.ContinueStage();
	}
	public void OnClickReStart()
	{
		GameMng.stageLevel = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}
	public void OnClickNextLevel()
	{
		GameMng.worldLevel++;
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}
	public void OnClickLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

	/* Player */
	public void AddGold(int gold)
    {
		playerInfoUI.AddGold(gold);
    }
	public void AddDiamond(int dia)
	{
		playerInfoUI.AddDiamond(dia);
	}
	public void AddEXP()
	{
		playerInfoUI.AddEXP();
	}
	/* Game */
	public void DamageToBoss(int damage, Vector3 pos)
	{
		damageTextPool.ShowDamage(damage, Camera.main.WorldToScreenPoint(pos));
		bossHealthGageImage.fillAmount = GameMng.Ins.monsterPool.GetBossFill();
	}
	public void SetBossInfo()
	{
		bossInfo.gameObject.SetActive(true);
		bossHealthGageImage.fillAmount = GameMng.Ins.monsterPool.GetBossFill();
	}
	public void AllClear()
	{
		bossInfo.gameObject.SetActive(false);
		ClearUI.gameObject.SetActive(true);
	}
	public void StageStart()
	{
		Fade(false);
		playerInfoUI.StageStart();
	}
	public void Fade(bool fadeFlag)
	{
		if(fadeFlag)
		{
			fade.DOColor(Color.black, 0.25f).OnComplete(() => { GameMng.Ins.StartStage(); });
		}
		else
		{
			fade.DOColor(Color.clear, 0.25f);
		}
	}
}
