using UnityEngine;
using GlobalDefine;
public class Flame : BulletPlayerSkill
{
	private float damage;
	private eAttackType attackType;
	public void Setting(eAttackType type,float _damage)
	{
		damage = _damage;
		attackType = type;
	}
	private void Update()
	{
		gameObject.transform.position = GameMng.Ins.player.transform.position + GameMng.Ins.player.GetForward() * 2f;
		gameObject.transform.eulerAngles = new Vector3(0, 0, GameMng.Ins.player.degree);
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(eAttackType.Fire, GameMng.Ins.player.calStat.damage, damage);
	}
}
