using System.Collections.Generic;
using UnityEngine;

public class SkillMng : MonoBehaviour
{
	public Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();
	private void Awake()
	{
		return;
		var playerSkillArr = JsonMng.Ins.playerInfoDataTable.setSkillList;
		for(int i = 0; i < playerSkillArr.Count; ++i)
		{
			if (playerSkillArr[i] == 0) continue;
			GameObject o = Instantiate(Resources.Load(string.Format("Skill/{0}", JsonMng.Ins.playerSkillDataTable[playerSkillArr[i]].skillName),typeof(GameObject)))as GameObject;
			o.transform.parent = gameObject.transform;
			Skill skill = o.GetComponent<Skill>();
			skill.SkillSetting();
			skillDict.Add(JsonMng.Ins.playerSkillDataTable[playerSkillArr[i]].skillID,skill);
		}
		var equip = JsonMng.Ins.playerInfoDataTable.equipItemList;
		for(int i = 0; i < equip.Length; ++i)
		{
			if (equip[i] == null) continue;
			if(skillDict.ContainsKey(equip[i].changeSkill))
			{
				skillDict[equip[i].changeSkill].SetItemBuff(equip[i].changeOption, equip[i].changeSkillValue);
			}
		}
		var eM = skillDict.GetEnumerator();
		while(eM.MoveNext())
		{
			eM.Current.Value.SetBullet();
		}
	}
	public void OffSkill()
	{
		var e = skillDict.GetEnumerator();
		while(e.MoveNext())
		{
			e.Current.Value.OffSkill();
		}
	}
	/* 테스트코드 */
	public void LoadAll()
	{
		var playerSkillArr = JsonMng.Ins.playerInfoDataTable.haveSkillList;
		skillDict.Clear();
		for (int i = 0; i < playerSkillArr.Count; ++i)
		{
			if (playerSkillArr[i] == 0) continue;
			GameObject o = Instantiate(Resources.Load(string.Format("Skill/{0}", JsonMng.Ins.playerSkillDataTable[playerSkillArr[i]].skillName), typeof(GameObject))) as GameObject;
			o.transform.parent = gameObject.transform;
			Skill skill = o.GetComponent<Skill>();
			skill.SkillSetting();
			skillDict.Add(JsonMng.Ins.playerSkillDataTable[playerSkillArr[i]].skillID, skill);
		}
		var equip = JsonMng.Ins.playerInfoDataTable.equipItemList;
		for (int i = 0; i < equip.Length; ++i)
		{
			if (equip[i] == null) continue;
			if (skillDict.ContainsKey(equip[i].changeSkill))
			{
				skillDict[equip[i].changeSkill].SetItemBuff(equip[i].changeOption, equip[i].changeSkillValue);
			}
		}
		var eM = skillDict.GetEnumerator();
		while (eM.MoveNext())
		{
			eM.Current.Value.SetBullet();
		}
	}
}
