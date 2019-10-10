using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
using System;

public class FireBall : MonoBehaviour
{
    float damage = 2;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;
    public bool colliderCrash = false;

    public void SetFireBall()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SetObj(collision));
    }

    private IEnumerator SetObj(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<MilliMonster>().Damage(attackType, damage);
            if(colliderCrash)
                gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
