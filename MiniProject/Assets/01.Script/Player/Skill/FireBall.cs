using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
using System;

public class FireBall : MonoBehaviour
{
    public float damage = 0;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            gameObject.SetActive(false);
        collision.GetComponent<MilliMonster>().Damage(attackType, damage);
        gameObject.SetActive(false);
    }
}
