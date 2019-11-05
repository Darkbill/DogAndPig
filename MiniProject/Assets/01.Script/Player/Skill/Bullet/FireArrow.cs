using UnityEngine;
using GlobalDefine;

public class FireArrow : BulletPlayerSkill
{
	private eAttackType attackType;
	private float damage;
	private float speed;
	private Vector3 dir;
	private float arrowActiveTime;
	private float cTime;
	public void Setting(eAttackType type, float _damage, float _speed,float _arrowActiveTime)
	{
		attackType = type;
		damage = _damage;
		speed = _speed;
		arrowActiveTime = _arrowActiveTime;
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
		cTime += Time.deltaTime;
		if(cTime >= arrowActiveTime)
		{
			cTime = 0;
			gameObject.SetActive(false);
			return;
		}
		gameObject.transform.position += dir * speed * Time.deltaTime;
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(attackType, GameMng.Ins.player.calStat.damage, damage);
		gameObject.SetActive(false);
		GameMng.Ins.HitToEffect(attackType, monster.transform.position, gameObject.transform.position);
	}
}
