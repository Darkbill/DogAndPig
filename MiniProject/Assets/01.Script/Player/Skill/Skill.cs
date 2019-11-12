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
		if (t >= 1)
		{
			gameObject.SetActive(false);
			return 1;
		}
		return delayTime / cooldownTime;
	}
	//각 스킬별 변화가능한 옵션 추가 계산
	public abstract void SetItemBuff(eSkillOption optionType, float changeValue);
	public abstract void SetBullet();


	public virtual void OnButtonDown()
	{

	}
	public virtual void OnDrag()
	{

	}
	public virtual void OnDrop()
	{

	}
	public abstract void OffSkill();
}
