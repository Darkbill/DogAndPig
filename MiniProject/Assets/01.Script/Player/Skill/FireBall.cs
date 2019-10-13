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
    public float SetTimer = 0.0f;
    public float falseTimer = 5.0f;

    public void SetFireBall()
    {

    }

    public void Update()
    {
        SetTimer += Time.deltaTime;
        if (SetTimer >= falseTimer)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<MilliMonster>().Damage(attackType, damage);
        gameObject.SetActive(false);
    }
}
