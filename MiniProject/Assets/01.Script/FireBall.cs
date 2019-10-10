using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class FireBall : MonoBehaviour
{
    float damage = 2;
    eAttackType attackType = eAttackType.Fire;

    public Vector3 BulletMovVec;

    public void SetFireBall()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<MilliMonster>().Damage(attackType, damage);
            gameObject.SetActive(false);
        }
    }
}
