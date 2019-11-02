using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
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

	private Vector3 stickPos; //터치 눌러서 joyStick이 시작한 위치
	private float stickRadius = 60;
    public Vector3 dir;

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
		GameMng.Ins.StartSkillAim();
		aimImage.SetTouchID(touchID);
		aimImage.gameObject.SetActive(true);
	}
	public void OnSkillDrop()
	{
		aimImage.gameObject.SetActive(false);
		CoolDownAllSkill();
	}
	public void OnSkillTouchDrop()
	{
		//모바일 빌드 스킬 터치 종료시에만 호출
		GameMng.Ins.EndSkillAim();
	}
	public void OffSkillAim()
	{
		aimImage.gameObject.SetActive(false);
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
    public void OnClickReStart()
    {
        //테스트코드
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }
    public void OnClickLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
    public void OnClickContinue()
    {

    }
    public void OnClickShop()
    {

    }

	/* Player */
	public void AddGold(int gold)
    {
		playerInfoUI.AddGold(gold);
    }
	public void AddEXP()
	{
		playerInfoUI.AddEXP();
	}
	/* Game */
	public void DamageToBoss(int damage)
	{
		damageTextPool.ShowDamage(damage, Camera.main.WorldToScreenPoint(gameObject.transform.position));
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
	public void StageClear()
	{
		playerInfoUI.StageClear();
	}
}
