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
		//Aim있는 스킬일 경우 Aim까지 종료했을 때 return true //스킬 발동시에만 AllCoolDown돌리기 위함
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
	public virtual void OffAim()
	{
		return;
	}
}
