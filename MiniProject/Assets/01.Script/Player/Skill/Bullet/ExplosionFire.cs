using UnityEngine;
using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
public class ExplosionFire : BulletPlayerSkill
{
	private eAttackType type;
	private float damage;
	public ParticleSystem explosion;
	private int skillID;
	public BoxCollider2D boxCol;
	public CircleCollider2D circleCol;
	private List<Monster> hitMonsterList = new List<Monster>();
	public void Setting(eAttackType _type, float _damage,int _skillID)
	{
		type = _type;
		damage = _damage;
		skillID = _skillID;
	}
	public void StartExPlosion(Vector3 pos)
	{
		hitMonsterList.Clear();
		gameObject.transform.position = pos;
		gameObject.SetActive(true);
		explosion.Play();
		StartCoroutine(OffCollider());
	}
	public override void Crash(Monster monster)
	{
		if (hitMonsterList.Contains(monster)) return;
		hitMonsterList.Add(monster);
		monster.Damage(type, GameMng.Ins.player.calStat.damage,damage);
		monster.OutStateAdd(new ConditionData(eBuffType.NockBack, skillID, 0, 2), monster.transform.position - transform.position);
	}
	private IEnumerator OffCollider()
	{
		boxCol.enabled = true;
		circleCol.enabled = true;
		yield return null;
		boxCol.enabled = false;
		circleCol.enabled = false;
	}
}
