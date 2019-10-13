using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezen : MonoBehaviour
{
    public int Id = 0;
    public float MaxTimer = 10.0f;
    public float slow = 0.0f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().Damage(eAttackType.Water, 1);
            collision.GetComponent<Monster>().state.SkillBufDebuf(
                new State(eAttackType.Water, Id, MaxTimer, new Vector3(slow / 1000, 0, 0)));
        }
    }
}
