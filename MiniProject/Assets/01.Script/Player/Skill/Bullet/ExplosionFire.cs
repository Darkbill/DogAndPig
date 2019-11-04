using UnityEngine;
using GlobalDefine;
using System.Collections;
public class ExplosionFire : BulletPlayerSkill
{
	private eAttackType type;
	private float damage;
	public ParticleSystem explosion;
	private int skillID;
	public BoxCollider2D boxCol;
	public void Setting(eAttackType _type, float _damage,int _skillID)
	{
		type = _type;
		damage = _damage;
		skillID = _skillID;
	}
	public void StartExPlosion(Vector3 pos)
	{
		gameObject.transform.position = pos;
		gameObject.SetActive(true);
		explosion.Play();
		StartCoroutine(OffCollider());
	}
	public override void Crash(Monster monster)
	{
		monster.Damage(type, GameMng.Ins.player.calStat.damage,damage);
		monster.OutStateAdd(new ConditionData(eBuffType.NockBack, skillID, 2, 0),transform.position - monster.transform.position);
	}
	private IEnumerator OffCollider()
	{
		boxCol.enabled = true;
		yield return null;
		boxCol.enabled = false;
	}
}
