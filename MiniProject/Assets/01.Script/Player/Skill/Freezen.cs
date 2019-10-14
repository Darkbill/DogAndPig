using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezen : MonoBehaviour
{
    public float damage = 0;
    eAttackType attackType = eAttackType.Water;

    public int Id = 0;
    public float MaxTimer = 10.0f;
    public float slow = 0.0f;

	public void Setting(int id,float mT,float s)
	{
		Id = id;
		MaxTimer = mT;
		slow = s;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
			//TODO 속성
            //collision.GetComponent<Monster>().Damage(eAttackType.Water, 1);
            collision.GetComponent<MilliMonster>().Damage(attackType, damage);
        }
    }
}
