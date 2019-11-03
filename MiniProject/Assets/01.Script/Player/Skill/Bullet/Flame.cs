using UnityEngine;
using GlobalDefine;
public class Flame : BulletPlayerSkill
{
	private float damage;
	private eAttackType attackType;
	public BoxCollider2D frameCollider;
	private float attackTime = 0.1f;
	private float cTime = 0;
	public void Setting(eAttackType type,float _damage)
	{
		damage = _damage;
		attackType = type;
	}
	private void OnDisable()
	{
		cTime = 0;
	}
	private void Update()
	{
		cTime += Time.deltaTime;
		if(cTime >= attackTime)
		{
			frameCollider.enabled = !frameCollider.enabled;
			cTime = 0;
		}
		gameObject.transform.position = GameMng.Ins.player.transform.position + GameMng.Ins.player.GetForward() * 2f;
		gameObject.transform.eulerAngles = new Vector3(0, 0, GameMng.Ins.player.degree);
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(eAttackType.Fire, GameMng.Ins.player.calStat.damage, damage);
	}
}
