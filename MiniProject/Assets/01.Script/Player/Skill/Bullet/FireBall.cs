using UnityEngine;
using GlobalDefine;

public class FireBall : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;

	public void Setting(Vector3 pos,Vector3 moveVec)
	{
		gameObject.transform.position = pos;
		BulletMovVec = moveVec;
		gameObject.SetActive(true);
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
