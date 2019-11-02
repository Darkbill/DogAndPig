﻿using System.Collections;
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
	public DamageTextPool damageTextPool;
	public Aim aimImage;

	/* Image */
	public Image stickImage;
	public Image expImage;
	public Image healthGageImage;
	public Image bossHealthGageImage;
	public Image coinImage;


	/* Text */
	public Text healthText;
	public Text coinText;
	public Text levelText;
	public Text stageLevelText;
	public Text nextStageText;

	private Vector3 stickPos; //터치 눌러서 joyStick이 시작한 위치
	private Vector3 healthGageImagePos;
	private Coroutine fillCoroutine;
	private float saveDamage = 0;
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
		levelText.text = GameMng.Ins.player.calStat.level.ToString();
		healthGageImagePos = healthGageImage.transform.position;
		healthText.text = string.Format("{0} / {1} ", GameMng.Ins.player.calStat.healthPoint,
            GameMng.Ins.player.GetFullHP());
		coinText.text = JsonMng.Ins.playerInfoDataTable.gold.ToString();
		skillUI.Setting();
	}
	public void RenewPlayerInfo()
	{
		levelText.text = GameMng.Ins.player.calStat.level.ToString();
		float cHp = GameMng.Ins.player.calStat.healthPoint;
		if (cHp < 0) cHp = 0;
		healthText.text = string.Format("{0} / {1} ", cHp,
			GameMng.Ins.player.GetFullHP());
		
		healthGageImage.fillAmount = cHp / GameMng.Ins.player.GetFullHP();
		stageLevelText.text = GameMng.Ins.stageLevel.ToString();
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
        ShowDamage(damage, Camera.main.WorldToScreenPoint(GameMng.Ins.player.transform.position));
		OnPlayerDamageHPShake();
		if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
		fillCoroutine = StartCoroutine(IEDamageToPlayer(damage, saveDamage, 0.5f));
        healthText.text = string.Format("{0} / {1} ", GameMng.Ins.player.calStat.healthPoint, GameMng.Ins.player.GetFullHP());
    }
    public void ShowDamage(int damage, Vector3 pos)
    {
        damageTextPool.ActiveDamageText(damage, pos);
    }
    IEnumerator IEDamageToPlayer(int damage, float save, float duration)
    {
        saveDamage = damage + save;
        double cTime = 0;
        //전체 체력대비 깍아야하는 체력의 비율
        double minus = saveDamage / GameMng.Ins.player.calStat.healthPoint;
        while (cTime < duration)
        {
            cTime += Time.deltaTime;
            saveDamage -= saveDamage * (Time.deltaTime / duration);
            //현채 fill에서 추가로 깎는다 ~초 까지
            healthGageImage.fillAmount -= (float)minus * (Time.deltaTime / duration);
            if (healthGageImage.fillAmount < GameMng.Ins.player.calStat.healthPoint / GameMng.Ins.player.GetFullHP()) break;
            yield return null;
        }
        healthGageImage.fillAmount = GameMng.Ins.player.calStat.healthPoint / GameMng.Ins.player.GetFullHP();
        saveDamage = 0;
        fillCoroutine = null;
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
        int c = int.Parse(coinText.text);
        c += gold;
        coinText.text = c.ToString();
        coinImage.transform.DOScale(Define.upscaleDuration, 0.1f).OnComplete(() => { 
            coinImage.transform.DOScale(1, 0.3f); });
    }
	public void AddEXP()
	{
		expImage.fillAmount = GameMng.Ins.player.GetEXPFill();
	}
	private void OnPlayerDamageHPShake()
    {
		healthGageImage.transform.position = healthGageImagePos;
		healthGageImage.transform.DOShakePosition(0.1f, 10.0f, 10, 90, false, true).OnComplete(()=> { healthGageImage.transform.position = healthGageImagePos; });
    }

	/* Game */
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
		stageLevelText.text = GameMng.Ins.stageLevel.ToString();
		stageLevelText.transform.localScale = new Vector3(2, 2, 2);
		stageLevelText.transform.DOScale(1, 0.5f);
		nextStageText.gameObject.SetActive(true);
		nextStageText.gameObject.transform.DOMoveX(gameObject.transform.position.x + 350, 0.4f).OnComplete(() => {
			nextStageText.gameObject.transform.DOScale(2, 1f);
			nextStageText.DOColor(new Color(1, 1, 1, 0), 1f).OnComplete(() => {
				nextStageText.color = Color.white;
				nextStageText.transform.localScale = Vector3.one;
				nextStageText.transform.position = new Vector3(nextStageText.transform.position.x - 350, nextStageText.transform.position.y, nextStageText.transform.position.z);
				nextStageText.gameObject.SetActive(false);
				GameMng.Ins.StartGame();
			});
		});
	}
}
