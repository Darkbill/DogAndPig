using UnityEngine;
using GlobalDefine;
public class Flame : BulletPlayerSkill
{
	private float damage;
	private eAttackType attackType;
	public BoxCollider2D frameCollider;
	private Vector3 dir;
	private float speed;
	private float upScaleSpeed;
	private float activeTime;
	private float cTime;
	private float reducationSpeed;
	private const float startScale = 0.75f;
	public void Setting(eAttackType type,float _damage, float _upScaleSpeed,float _activeTime,float reSpeed)
	{
		damage = _damage;
		attackType = type;
		upScaleSpeed = _upScaleSpeed;
		activeTime = _activeTime;
		reducationSpeed = reSpeed;
	}
	public void Setting(Vector3 pos,float _speed,Vector3 _dir,float degree)
	{
		cTime = 0;
		speed = _speed;
		gameObject.transform.localScale = new Vector3(startScale, startScale, startScale);
		gameObject.SetActive(true);
		float size = GameMng.Ins.player.calStat.size;
		transform.position = pos + GameMng.Ins.player.GetForward() * size + new Vector3(0,size,0);
		dir = _dir;
		transform.eulerAngles = new Vector3(0, 0, degree);
	}
	private void Update()
	{
		gameObject.transform.position += dir * Time.deltaTime * speed;
		speed -= Time.deltaTime * reducationSpeed;
		gameObject.transform.localScale += new Vector3(startScale, startScale, startScale) * Time.deltaTime * upScaleSpeed;
		cTime += Time.deltaTime;
		if(cTime >= activeTime)
		{
			gameObject.SetActive(false);
		}
	}
	public override void Crash(Monster monster)
	{
		if (monster.active == false) return;
		monster.Damage(eAttackType.Fire, GameMng.Ins.player.calStat.damage, damage);
		GameMng.Ins.HitToEffect(attackType, monster.transform.position, gameObject.transform.position - new Vector3(0.6f, 0), monster.monsterData.size);
	}
}
