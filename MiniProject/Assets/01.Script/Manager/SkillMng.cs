using System.Collections.Generic;
using UnityEngine;

public class SkillMng : MonoBehaviour
{
	public Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();
	private void Awake()
	{
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
	}
	public void OffSkill()
	{
		var e = skillDict.GetEnumerator();
		while(e.MoveNext())
		{
			e.Current.Value.gameObject.SetActive(false);
		}
	}
}
