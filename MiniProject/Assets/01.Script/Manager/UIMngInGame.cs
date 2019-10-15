using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
using System;
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
    /* Player Controller UI */
    public Image stickImage;
    public GameObject joyStick;
    public Vector3 stickPos;
    public float stickRadius = 60;
    public int moveTouchID;
    public DamageTextPool damageTextPool;

    /* Player Infomation UI*/
    public GameObject healthPack;
    public Image healthGageImage;
    public Text healthText;
    public Coroutine fillCoroutine;
    public BuffUI buffUI;
    public float saveDamage = 0;

    public Image coinImage;
    public Text coinText;

    /* Skill UI */
    public Image[] skillImageArr;
    bool isCool = false;
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
        healthText.text = string.Format("{0} / {1} ", GameMng.Ins.player.calStat.healthPoint,
            GameMng.Ins.player.GetFullHP());
        //테스트코드
        //for(int i = 0; i < GameMng.Ins.player.skillArr.Length;++i)
        //{
        //	skillImageArr[i].sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", GameMng.Ins.player.skillArr[i]));
        //}
    }
    public void OnStrickDrag()
    {
        GameMng.Ins.player.isMove = true;
        joyStick.gameObject.SetActive(true);
        joyStick.gameObject.transform.position = Input.mousePosition;
        stickPos = stickImage.gameObject.transform.position;
    }
    public void OnStrickDrag(Touch touch)
    {
        GameMng.Ins.player.isMove = true;
        joyStick.gameObject.SetActive(true);
        moveTouchID = touch.fingerId;
        joyStick.gameObject.transform.position = touch.position;
        stickPos = stickImage.gameObject.transform.position;
    }
    public void OnStickDrop()
    {
        GameMng.Ins.player.isMove = false;
        joyStick.gameObject.SetActive(false);
        moveTouchID = -1;
    }
    /* Skill */
    public void ActiveSkill(int slotNumber)
    {
        if (skillImageArr[slotNumber].fillAmount == 1)
        {
            GameMng.Ins.ActiveSkill(GameMng.Ins.player.skillArr[slotNumber]);
            CoolDownAllSkill();
        }
    }
    public void CoolDownAllSkill()
    {
        isCool = true;
        StartCoroutine(IECoolDownAllSkill());
    }
    private IEnumerator IECoolDownAllSkill()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= Define.coolDownTimeAll)
            {
                isCool = false;
                break;
            }
            for (int i = 0; i < GameMng.Ins.player.skillArr.Length; ++i)
            {
                skillImageArr[i].fillAmount = (timer / Define.coolDownTimeAll);
            }
            yield return null;
        }
    }
    public Vector3 GetJoyStickDirection()
    {
        /* 컴퓨터 빌드 */
#if UNITY_EDITOR_WIN
        stickImage.gameObject.transform.position = Input.mousePosition;
        Vector3 dir = stickImage.gameObject.transform.position - stickPos;
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

			if (tempTouchs.fingerId == moveTouchID)
			{
				stickImage.gameObject.transform.position = tempTouchs.position;
				Vector3 dir = stickImage.gameObject.transform.position - stickPos;
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
    //TODO 제네릭^^ 몬스터스테이트까지 전부
    public void DamageToPlayer(int damage)
    {
        ShowDamage(damage, Camera.main.WorldToScreenPoint(GameMng.Ins.player.transform.position));
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
        OnPlayerDamageHPShake();
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
    private void Update()
    {
        //TODO : 이건아닌데..
        if (isCool == false)
        {
            for (int i = 0; i < GameMng.Ins.player.skillArr.Length; ++i)
            {
                skillImageArr[i].fillAmount = GameMng.Ins.skillMng.skillDict[GameMng.Ins.player.skillArr[i]].GetDelay(); ;
            }
        }
    }
    public void GameOver()
    {
        gameOverUI.gameObject.SetActive(true);
    }
    public void OnClickReStart()
    {
        //테스트코드
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }
    public void OnClickContinue()
    {

    }
    public void OnClickShop()
    {

    }

    public void OnCoinSelectInGame(float upScaleDuration)
    {
        int c = int.Parse(coinText.text);
        ++c;
        coinText.text = c.ToString();
        coinImage.transform.DOScale(upScaleDuration, 0.1f).OnComplete(() => { 
            coinImage.transform.DOScale(1, 0.3f); });
    }

    private void OnPlayerDamageHPShake()
    {
        healthPack.transform.DOShakePosition(0.3f, 20.0f, 10, 90, false, true);
    }
}
