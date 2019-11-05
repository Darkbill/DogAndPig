using UnityEngine;
using GlobalDefine;

public class FireArrow : BulletPlayerSkill
{
	private eAttackType attackType;
	private float damage;
	private float speed;
	private Vector3 dir;
	public void Setting(eAttackType type, float _damage, float _speed)
	{
		attackType = type;
		damage = _damage;
		speed = _speed;
		gameObject.SetActive(false);
	}
	public void Setting(Vector3 _dir,Vector3 startPos,float zAngle)
	{
		dir = _dir;
		gameObject.transform.eulerAngles = new Vector3(zAngle, 0, 0);
		transform.position = transform.position = startPos;
		transform.position += new Vector3(dir.y * Random.Range(-0.5f, 0.5f), dir.x * Random.Range(-0.5f, 0.5f), 0);
		gameObject.SetActive(true);
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
