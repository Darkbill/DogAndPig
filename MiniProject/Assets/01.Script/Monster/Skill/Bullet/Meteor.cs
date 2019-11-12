using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 pos = new Vector3();
    private const float radius = 0.8f;
    private const float damage = 10.0f;
    public void Setting()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        OnPlayerEnterHit();
    }

    private void OnPlayerEnterHit()
    {
        float range = (gameObject.transform.position - GameMng.Ins.player.transform.position).magnitude;
        if(range < radius)
        {
            GameMng.Ins.player.Damage(GlobalDefine.eAttackType.Fire, damage);
        }
    }

    [Obsolete]
    private void Update()
    {
        if (gameObject.GetComponent<ParticleSystem>().isPlaying == false)
            gameObject.SetActive(false);
    }
    public void EndAnimation() { gameObject.SetActive(false); }
}
