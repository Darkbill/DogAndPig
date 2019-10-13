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
	public virtual void ActiveSkill()
	{
		gameObject.SetActive(true);
		delayTime = 0;
	}
	private void Update()
	{
		//TODO : 이건아닌데..
		delayTime += Time.deltaTime;
	}
}
