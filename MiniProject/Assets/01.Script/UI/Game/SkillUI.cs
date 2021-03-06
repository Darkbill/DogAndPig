﻿using UnityEngine;
using System.Collections;
using GlobalDefine;
using DG.Tweening;
public class SkillUI : MonoBehaviour
{
	public SkillIconInGame[] skillArr;
	private bool isCool = false;
	private int skillNum = 0;
	private bool isSkillAct = false;
	private Coroutine hightRightCoroutine;
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
	public void StartSkillSet(int _skillNum)
	{
        if (skillArr[_skillNum].skillImage.fillAmount == 1)
		{
			int skillID = JsonMng.Ins.playerInfoDataTable.setSkillList[_skillNum];
			if (skillID == 0) return;
			else if (skillID == GameMng.Ins.aimSkillID)
			{
				//Aim중인 스킬 재사용시 Aim종료
				GameMng.Ins.OffSkillAim();
				return;
			}
			else if (GameMng.Ins.aimSkillID != -1) GameMng.Ins.OffSkillAim();
			skillNum = _skillNum;
			GameMng.Ins.ActiveSkill(JsonMng.Ins.playerInfoDataTable.setSkillList[skillNum]);
		}
	}
	public void HightLightSkillSet(bool onCheck)
	{
		if (onCheck == true)
		{
			isSkillAct = true;
			hightRightCoroutine = StartCoroutine(SelectoffSet());
		}
		else
		{
			StopCoroutine(hightRightCoroutine);
			skillArr[skillNum].skillImage.color = Color.white;
		}
	}
	private IEnumerator SelectoffSet()
	{
		int skillnum = skillNum;

        while (isSkillAct)
		{
			skillArr[skillnum].skillImage.DOColor(Color.black, 0.5f).OnComplete(() =>
			{
				skillArr[skillnum].skillImage.DOColor(Color.white, 0.5f);
			});
			yield return new WaitForSeconds(1.0f);
		}
	}
}
