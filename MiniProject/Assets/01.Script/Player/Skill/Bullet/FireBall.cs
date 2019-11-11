using UnityEngine;
using GlobalDefine;
using System.Collections;

public class FireBall : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;
    public float angle;
    private bool rotateset = false;
    private float speed = 5;

	public void Setting(Vector3 pos,Vector3 moveVec)
	{
		gameObject.transform.position = pos;
		BulletMovVec = moveVec;
		gameObject.SetActive(true);
        StartCoroutine(setSkill());
        rotateset = false;
	}

    private void Update()
    {
        StartCoroutine(CircleSet());
        if(!rotateset)
        {
            gameObject.transform.position = GameMng.Ins.player.transform.position + BulletMovVec;
            BulletMovVec = Quaternion.Euler(0, 0, speed) * BulletMovVec;
        }
        else
            gameObject.transform.position += BulletMovVec * Time.deltaTime * speed;
    }

    private IEnumerator CircleSet()
    {
        yield return new WaitForSeconds(2.0f);
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
