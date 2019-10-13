using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
using System;

public class FireBall : MonoBehaviour
{
    public float damage = 2;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<MilliMonster>().Damage(attackType, damage);
        gameObject.SetActive(false);
    }
}
