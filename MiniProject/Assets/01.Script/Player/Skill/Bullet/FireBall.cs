using UnityEngine;
using GlobalDefine;
using System.Collections;

public class FireBall : BulletPlayerSkill
{
    public float damage;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;
    public float angle;
    private bool rotateset = false;
	private float speed;
	private float duration;
	private float durationTime; //쉴드유지시간
	private float delayTime;
	public void Setting(float _damage, float _speed, float _duration, float _durationTime)
	{
		damage = _damage;
		speed = _speed;
		duration = _duration;
		durationTime = _durationTime;
		gameObject.transform.parent = GameMng.Ins.skillMng.transform;
		gameObject.SetActive(false);
	}
	public void SystemSetting(Vector3 pos,Vector3 moveVec,Vector3 angle)
	{
		delayTime = 0;
		gameObject.transform.position = pos;
		gameObject.transform.eulerAngles = angle;
		BulletMovVec = moveVec;
		gameObject.SetActive(true);
        rotateset = false;
	}

    private void Update()
    {
		if (delayTime >= duration)
		{
			gameObject.SetActive(false);
		}
		delayTime += Time.deltaTime;
		if(gameObject.activeSelf == true) StartCoroutine(CircleSet());
		if (!rotateset)
        {
            gameObject.transform.position = GameMng.Ins.player.transform.position + BulletMovVec;
            BulletMovVec = Quaternion.Euler(0, 0, speed) * BulletMovVec;
        }
        else
            gameObject.transform.position += BulletMovVec * Time.deltaTime * speed;
    }

    private IEnumerator CircleSet()
    {
        yield return new WaitForSeconds(durationTime);
        rotateset = true;
    }
    public override void Crash(Monster monster)
	{
		monster.Damage(attackType, GameMng.Ins.player.calStat.damage, damage);
		GameMng.Ins.HitToEffect(attackType, 
            monster.transform.position + new Vector3(0, monster.monsterData.size), 
            gameObject.transform.position + new Vector3(-0.3f, -0.15f),
            monster.monsterData.size);
		gameObject.SetActive(false);
	}
}
