using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMng : MonoBehaviour
{
	public Dictionary<float, Skill> skillDict = new Dictionary<float, Skill>();
	private void Awake()
	{
		Skill[] skills = gameObject.GetComponentsInChildren<Skill>(true);
		for(int i = 0; i < skills.Length; ++i)
		{
			skills[i].SkillSetting();
			skills[i].gameObject.SetActive(false);
			skillDict.Add(skills[i].skillID, skills[i]);
		}
	}
}
