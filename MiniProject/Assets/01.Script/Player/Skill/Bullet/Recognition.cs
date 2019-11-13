using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recognition : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType attackType = eAttackType.Physics;

    public Vector3 bulletMovevec;
    private float speed;

    private bool turnOn = false;
    private float maxRange;
    private float pokRange = 0;

    public ParticleSystem particle;
	public void Setting(float _damage,float _speed,float _maxRange)
	{
		damage = _damage;
		speed = _speed;
		maxRange = _maxRange;
		gameObject.SetActive(false);

	}
	public void Setting(Vector3 pos, Vector3 moveVec)
    {
		gameObject.SetActive(true);
		gameObject.transform.position = pos;
        bulletMovevec = moveVec;
        pokRange = 0;
        particle.Play();
        turnOn = false;
    }

    private void Update()
    {
        if (!particle.isPlaying)
            gameObject.SetActive(false);
        if(!turnOn)
        {
            gameObject.transform.position += bulletMovevec * speed * Time.deltaTime;
            pokRange += speed * Time.deltaTime;
            if (pokRange >= maxRange)
            {
                turnOn = true;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        else
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            Vector3 pos = GameMng.Ins.player.transform.position - gameObject.transform.position;
            pos = pos.normalized;
            gameObject.transform.position += pos * speed * Time.deltaTime;
            if ((gameObject.transform.position - GameMng.Ins.player.transform.position).magnitude < GameMng.Ins.player.calStat.size)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public override void Crash(Monster monster)
    {
        monster.Damage(attackType, GameMng.Ins.player.calStat.damage, damage);
        GameMng.Ins.HitToEffect(attackType,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position + new Vector3(-0.3f, -0.15f),
            monster.monsterData.size);
    }
}
