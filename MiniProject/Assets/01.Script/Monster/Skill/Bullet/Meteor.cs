using GlobalDefine;
using System;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    eAttackType Attacktype = eAttackType.Fire;

    public Vector3 pos = new Vector3();
    public float radius;
    public float damage;

    public void Setting(float _radius, float _damage)
    {
        radius = _radius;
        damage = _damage;
    }

    public void SystemSetting()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        OnPlayerEnterHit();
    }

    private void OnPlayerEnterHit()
    {
        float range = (gameObject.transform.position - GameMng.Ins.player.transform.position).magnitude;
        if(range < radius)
        {
            GameMng.Ins.player.Damage(eAttackType.Fire, damage);
            GameMng.Ins.HitToEffect(Attacktype,
                        GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size),
                        gameObject.transform.position,
                        GameMng.Ins.player.calStat.size);
        }
    }

    private void Update()
    {
        if (gameObject.GetComponent<ParticleSystem>().isPlaying == false)
            gameObject.SetActive(false);
    }
    public void EndAnimation() { gameObject.SetActive(false); }
}
