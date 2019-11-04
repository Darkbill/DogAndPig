using UnityEngine;
using GlobalDefine;

public class FireArrow : BulletPlayerSkill
{
	private eAttackType attackType;
	private float damage;
	private float speed;
	private Vector3 dir;
	public void Setting(eAttackType type,float _damage,float _speed,Vector3 _dir)
	{
		attackType = type;
		damage = _damage;
		speed = _speed;
		dir = _dir;
	}
	private void Update()
	{
		gameObject.transform.position += dir * speed * Time.deltaTime;
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(attackType, GameMng.Ins.player.calStat.damage, damage);
		gameObject.SetActive(false);
		//TODO : 피격이펙트
	}
}
