using UnityEngine;
using GlobalDefine;
public abstract class Skill : MonoBehaviour
{
	public int skillID;
	public string skillName;
	protected eAttackType skillType;
	protected eSkillType target;
	public float cooldownTime;
	public float delayTime;
	abstract public void SkillSetting();
	public virtual bool ActiveSkill()
	{
		gameObject.SetActive(true);
		delayTime = 0;
		return true;
	}
	public float GetDelay()
	{
		float t = delayTime / cooldownTime;
		if(t >= 1)
		{
			gameObject.SetActive(false);
			return 1;
		}
		return delayTime / cooldownTime;
	}

}
