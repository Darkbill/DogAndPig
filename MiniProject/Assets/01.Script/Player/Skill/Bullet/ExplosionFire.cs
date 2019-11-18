using UnityEngine;
using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
public class ExplosionFire : BulletPlayerSkill
{
	public ParticleSystem explosion;
	public BoxCollider2D boxCol;
	public CircleCollider2D circleCol;
	public GameObject thrower;

	private List<Monster> hitMonsterList = new List<Monster>();
	private eAttackType type = eAttackType.Fire;
	private float damage;
	private int skillID;
	private float knockBackPower;
	private float throwTime;
	private float upScale;
	public void Setting(float _damage,int _skillID, float _knockBackPower,float _throwTime,float _upScale)
	{
		damage = _damage;
		skillID = _skillID;
		knockBackPower = _knockBackPower;
		throwTime = _throwTime;
		upScale = _upScale;
		gameObject.transform.parent = GameMng.Ins.skillMng.transform;
		gameObject.SetActive(false);
	}
	public override void Crash(Monster monster)
	{
		if (hitMonsterList.Contains(monster)) return;
		hitMonsterList.Add(monster);
		monster.Damage(type, GameMng.Ins.player.calStat.damage,damage);
		monster.OutStateAdd(new ConditionData(eBuffType.NockBack, skillID, 0, knockBackPower), monster.transform.position - transform.position);
	}
	private IEnumerator OffSkill()
	{
		yield return new WaitForSeconds(1);
		ActiveOff();
	}
	public void ActiveOff()
	{
		explosion.gameObject.SetActive(false);
		thrower.gameObject.SetActive(false);
	}
	private IEnumerator OffCollider()
	{
		boxCol.enabled = true;
		circleCol.enabled = true;
		yield return null;
		boxCol.enabled = false;
		circleCol.enabled = false;
	}
	internal void StartThrow()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos = new Vector3(mousePos.x, mousePos.y, 0);
		thrower.SetActive(true);
		StartCoroutine(Throw(mousePos));
	}
	internal void StartThrow(Vector2 pos)
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(pos);
		mousePos = new Vector3(mousePos.x, mousePos.y, 0);
		thrower.SetActive(true);
		StartCoroutine(Throw(mousePos));
	}
	IEnumerator Throw(Vector3 pos)
	{
		thrower.gameObject.transform.position = GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size, 0);
		Vector3 dir = pos - thrower.gameObject.transform.position;
		float m = dir.magnitude;
		float cTime = 0;
		Vector3 changeVec = new Vector3(0, upScale, 0);
		Vector3 copy = new Vector3(0, upScale, 0);
		dir.Normalize();
		while (cTime <= throwTime)
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
			if ((pos - thrower.gameObject.transform.position).magnitude <= 0.3f)
			{
				thrower.SetActive(false);
				StartExPlosion(thrower.transform.position);
				yield break;
			}
			else yield return null;
		}
	}
	public void StartExPlosion(Vector3 pos)
	{
		hitMonsterList.Clear();
		gameObject.transform.position = pos;
		explosion.gameObject.SetActive(true);
		explosion.time = 0;
		explosion.Play();
		StartCoroutine(OffCollider());
		StartCoroutine(OffSkill());
	}

}
