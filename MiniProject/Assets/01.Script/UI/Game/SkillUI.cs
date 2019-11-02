using UnityEngine;
using System.Collections;
using GlobalDefine;
using DG.Tweening;
public class SkillUI : MonoBehaviour
{
	public SkillIconInGame[] skillArr;
	private bool isCool = false;
	private int skillNum = 0;
	private bool isSkillAct = false;
	private void Update()
	{
		if (isCool == false)
		{
			for (int i = 0; i < JsonMng.Ins.playerInfoDataTable.setSkillList.Count; ++i)
			{
				int skillID = JsonMng.Ins.playerInfoDataTable.setSkillList[i];
				if (skillID == 0) continue;
				skillArr[i].ChangeFill(GameMng.Ins.skillMng.skillDict[skillID].GetDelay());
			}
		}
	}
	public void Setting()
	{
		for (int i = 0; i < JsonMng.Ins.playerInfoDataTable.setSkillList.Count; ++i)
		{
			skillArr[i].Setting(JsonMng.Ins.playerInfoDataTable.setSkillList[i]);
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
			for (int i = 0; i < JsonMng.Ins.playerInfoDataTable.setSkillList.Count; ++i)
			{
				skillArr[i].ChangeFill(timer / Define.coolDownTimeAll);
			}
			yield return null;
		}
	}
	public void StartSkillSet(int skillnum)
	{
        isSkillAct = false;
        skillArr[skillNum].skillImage.DOColor(Color.white, 0.5f);
        skillNum = skillnum;
        //TODO : 이전의 스킬 index값을 받아와서 이전의 스킬 id가 Aim이라면 종료 후 현재스킬 사용.
        //만약 현재스킬이 Aim스킬이라면 그대로 유지. << 좋은 방법일까?
		if (skillArr[skillnum].skillImage.fillAmount == 1)
		{
			int skillID = JsonMng.Ins.playerInfoDataTable.setSkillList[skillnum];
			if (skillID == 0) return;
			else if (skillID == GameMng.Ins.aimSkillID)
			{
				//Aim중인 스킬 재사용시 Aim종료
				GameMng.Ins.OffSkillAim();
				return;
			}
			if (GameMng.Ins.ActiveSkill(skillID)) CoolDownAllSkill();
		}
	}
	public void HightLightSkillSet(bool onCheck)
	{
		isSkillAct = onCheck;
		StartCoroutine(SelectoffSet());
	}
	private IEnumerator SelectoffSet()
	{
		while (isSkillAct)
		{
			skillArr[skillNum].skillImage.DOColor(Color.black, 0.5f).OnComplete(() =>
			{
				skillArr[skillNum].skillImage.DOColor(Color.white, 0.5f);
			});
			yield return new WaitForSeconds(1.0f);
		}
		skillArr[skillNum].skillImage.color = Color.white;
		yield break;
	}
}
