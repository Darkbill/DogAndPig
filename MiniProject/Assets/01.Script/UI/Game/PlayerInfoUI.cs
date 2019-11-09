using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class PlayerInfoUI : MonoBehaviour
{
	public Text healthText;
	public Text coinText;
	public Text levelText;
	public Text stageLevelText;
	public Text expText;
	public Text diaText;
	public Image healthGageImage;
	public Image coinImage;
	public Image diamondImage;
	public Image expImage;
	private Vector3 healthGageImagePos;
	private Coroutine fillCoroutine;
	private float saveDamage = 0;

	public void Setting()
	{
		RenewPlayerInfo();
		healthGageImagePos = healthGageImage.transform.position;
	}
	public void RenewPlayerInfo()
	{
		levelText.text = JsonMng.Ins.playerInfoDataTable.playerLevel.ToString();
		coinText.text = JsonMng.Ins.playerInfoDataTable.gold.ToString();
		diaText.text = JsonMng.Ins.playerInfoDataTable.diamond.ToString();
		float cHp = GameMng.Ins.player.calStat.healthPoint;
		if (cHp < 0) cHp = 0;
		healthText.text = string.Format("{0} / {1} ", cHp,
			GameMng.Ins.player.GetFullHP());
		healthGageImage.fillAmount = cHp / GameMng.Ins.player.GetFullHP();
		stageLevelText.text = GameMng.stageLevel.ToString();
		expImage.fillAmount = GameMng.Ins.player.GetEXPFill();
#if UNITY_EDITOR_WIN
		expText.text = string.Format("{0} / {1}", JsonMng.Ins.playerInfoDataTable.exp + JsonMng.Ins.expDataTable[JsonMng.Ins.playerInfoDataTable.playerLevel].cumulativeExp, (JsonMng.Ins.expDataTable[JsonMng.Ins.playerInfoDataTable.playerLevel + 1].cumulativeExp));
#else
		expText.text = string.Format("{0} / {1}", JsonMng.Ins.playerInfoDataTable.exp, (JsonMng.Ins.expDataTable[JsonMng.Ins.playerInfoDataTable.playerLevel + 1].requiredExp));
#endif
	}
	public void DamageToPlayer(int damage)
	{
		OnPlayerDamageHPShake();
		if (fillCoroutine != null)
		{
			StopCoroutine(fillCoroutine);
		}
		fillCoroutine = StartCoroutine(IEDamageToPlayer(damage, saveDamage, 0.5f));
		float cHp = GameMng.Ins.player.calStat.healthPoint;
		if (cHp < 0) cHp = 0;
		healthText.text = string.Format("{0} / {1} ", cHp,
			GameMng.Ins.player.GetFullHP());
	}
	IEnumerator IEDamageToPlayer(int damage, float save, float duration)
	{
		saveDamage = damage + save;
		double cTime = 0;
		//전체 체력대비 깍아야하는 체력의 비율
		double minus = saveDamage / GameMng.Ins.player.calStat.healthPoint;
		if (minus > 1) minus = 1;
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
	private void OnPlayerDamageHPShake()
	{
		healthGageImage.transform.position = healthGageImagePos;
		healthGageImage.transform.DOShakePosition(0.1f, 10.0f, 10, 90, false, true).OnComplete(() => { healthGageImage.transform.position = healthGageImagePos; });
	}
	public void AddGold(int gold)
	{
		int c = int.Parse(coinText.text);
		c += gold;
		coinText.text = c.ToString();
		coinImage.transform.DOScale(Define.upscaleDuration, 0.1f).OnComplete(() => {
			coinImage.transform.DOScale(1, 0.3f);
		});
	}
	public void AddDiamond(int dia)
	{
		int c = int.Parse(diaText.text);
		c += dia;
		diaText.text = c.ToString();
		diamondImage.transform.DOScale(Define.upscaleDuration, 0.1f).OnComplete(() => {
			diamondImage.transform.DOScale(1, 0.3f);
		});
	}
	public void AddEXP()
	{
		expImage.fillAmount = GameMng.Ins.player.GetEXPFill();
	}
	public void StageStart()
	{
		stageLevelText.text = GameMng.stageLevel.ToString();
		stageLevelText.transform.localScale = new Vector3(2, 2, 2);
		stageLevelText.transform.DOScale(1, 0.5f);
		//TODO : NextStage연출
	}
}
