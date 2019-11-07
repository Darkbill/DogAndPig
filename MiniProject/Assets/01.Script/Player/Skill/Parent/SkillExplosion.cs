using UnityEngine;
using System.Collections;
using GlobalDefine;
public class SkillExplosion : Skill
{
	public ExplosionFire explosionFire;
	public GameObject thrower;
	#region SkillSetting
	enum eExplosionSkillOption
	{
		Damage,
		CoolTime,
		ThrowTime,
		UpScale,
		KnockBackPower,
	}
	private float damage;
	private float throwTime;
	private float upScale;
	private float knockBackPower;
	public override void SkillSetting()
	{
		skillID = 9;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eExplosionSkillOption.Damage];
		cooldownTime = skillData.optionArr[(int)eExplosionSkillOption.CoolTime];
		throwTime = skillData.optionArr[(int)eExplosionSkillOption.ThrowTime];
		upScale = skillData.optionArr[(int)eExplosionSkillOption.UpScale];
		knockBackPower = skillData.optionArr[(int)eExplosionSkillOption.KnockBackPower];
		delayTime = cooldownTime;
		gameObject.SetActive(false);
	}
	public override void SetItemBuff(eSkillOption optionType, float changeValue)
	{
		switch (optionType)
		{
			case eSkillOption.Damage:
				damage += damage * changeValue;
				break;
			case eSkillOption.CoolTime:
				cooldownTime -= cooldownTime * changeValue;
				break;
			case eSkillOption.BuffChangeValue:
				knockBackPower += knockBackPower * changeValue;
				break;
		}
	}
	public override void SetBullet()
	{
		explosionFire.Setting(skillType, damage, skillID, knockBackPower);
	}
	#endregion
	//실제 쿨타임 도는 타이밍에 ActiveSkill();
	public override void OnButtonDown()
	{
		base.OnButtonDown();
		GameMng.Ins.SetSkillAim(skillID);
	}
	public override void OnDrag()
	{
		base.OnDrag();
		explosionFire.gameObject.SetActive(false);
	}
	public override void OnDrop()
	{
		base.OnDrop();
		ActiveSkill();
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos = new Vector3(mousePos.x, mousePos.y, 0);
		StartCoroutine(Throw(mousePos));
	}
	private void Update()
	{
		delayTime += Time.deltaTime;
	}
	public void Explosion(Vector3 pos)
	{
		explosionFire.StartExPlosion(pos);
	}
	IEnumerator Throw(Vector3 pos)
	{
		thrower.SetActive(true);
		thrower.gameObject.transform.position = GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size, 0);
		Vector3 dir = pos - thrower.gameObject.transform.position;
		float m = dir.magnitude;
		float cTime = 0;
		Vector3 changeVec = new Vector3(0, upScale, 0);
		Vector3 copy = new Vector3(0, upScale, 0);
		dir.Normalize();
		while(cTime <= throwTime)
		{
			cTime += Time.deltaTime;
			changeVec -= copy * (Time.deltaTime / throwTime);
			thrower.transform.position += dir * Time.deltaTime * m + changeVec;
			yield return null;
		}
		changeVec = Vector3.zero;
		while (true)
		{
			cTime += Time.deltaTime;
			changeVec -= copy * (Time.deltaTime / throwTime);
			thrower.transform.position += dir * Time.deltaTime * m + changeVec;
			if ((pos - thrower.gameObject.transform.position).magnitude <= 0.2f)
			{
				thrower.SetActive(false);
				Explosion(thrower.transform.position);
				yield break;
			}
			else yield return null;
		}
	}
}