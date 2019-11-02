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
		Debug.Log("에임 제거");
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
