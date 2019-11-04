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
	public bool activeFlag;
	abstract public void SkillSetting();
	public virtual void ActiveSkill()
	{
		UIMngInGame.Ins.CoolDownAllSkill();
		gameObject.SetActive(true);
		delayTime = 0;
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
	public virtual void OnButtonDown()
	{
		Debug.Log("스킬 시작");
	}
	public virtual void OnDrag()
	{
		Debug.Log("드래그 시작");
	}
	public virtual void OnDrop()
	{
		Debug.Log("마우스 드랍");
	}
}
