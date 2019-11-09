using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 pos = new Vector3();
    public void Setting()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damage(GlobalDefine.eAttackType.Fire, 10);
        }
    }

    [Obsolete]
    private void Update()
    {
        if (gameObject.GetComponent<ParticleSystem>().duration > 0.6f)
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        if (gameObject.GetComponent<ParticleSystem>().isPlaying == false)
            gameObject.SetActive(false);
    }
    public void EndAnimation() { gameObject.SetActive(false); }
}
