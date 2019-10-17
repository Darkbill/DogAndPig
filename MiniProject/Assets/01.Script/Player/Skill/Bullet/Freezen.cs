using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezen : MonoBehaviour
{
    public float damage = 0;
    eBuffType buffType = eBuffType.MoveSlow;

    private int Id = 0;
    private float MaxTimer = 10.0f;
    private float slow = 0.0f;
	private float per;

	public void Setting(int id,float mT,float s,float p)
	{
		Id = id;
		MaxTimer = mT;
		slow = s;
		per = p;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
			collision.GetComponent<MilliMonster>().Damage(eAttackType.Water, damage,new ConditionData(buffType, Id,MaxTimer,slow),per);
		}
    }
}
