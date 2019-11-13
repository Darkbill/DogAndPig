using GlobalDefine;
using UnityEngine;

public class Thounder : BulletPlayerSkill
{
    public float damage = 0;
    eAttackType Attacktype = eAttackType.Lightning;
    public bool hit = false;

    public ParticleSystem particle;

    public void Setting(float dmg)
    {
        damage = dmg;
		gameObject.transform.parent = GameMng.Ins.skillMng.transform;
        hit = false;
		gameObject.SetActive(false);
    }

    public void isPlay() { particle.Play(); }

    private void Update()
    {
        if (!particle.isPlaying)
            gameObject.SetActive(false);
    }

    public override void Crash(Monster monster)
    {
        hit = true;
		if (monster.active == false) return;
		monster.Damage(Attacktype,GameMng.Ins.player.calStat.damage, damage);
        GameMng.Ins.HitToEffect(Attacktype,
            monster.transform.position + new Vector3(0, monster.monsterData.size),
            gameObject.transform.position,
            monster.monsterData.size);
    }
}
