using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpFill : MonoBehaviour
{
	public Image hp;
	public float fHp = 100;
	public float cHp = 100;
	public Coroutine fillCoroutine;
	public float saveDamage = 0;
	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(fillCoroutine != null)
			{
				StopCoroutine(fillCoroutine);
			}
			fillCoroutine = StartCoroutine(Damage(10,saveDamage, 2));
		}
	}
	IEnumerator Damage(float damage,float save, float duration)
	{
		saveDamage = damage + save;
		cHp -= damage;
		float cTime = 0;
		//전체 체력대비 깍아야하는 체력의 비율
		float minus = saveDamage / fHp;
		while (cTime < duration)
		{
			cTime += Time.deltaTime;
			saveDamage -= saveDamage * (Time.deltaTime / duration);
			//현채 fill에서 추가로 깎는다 ~초 까지
			hp.fillAmount -= minus * (Time.deltaTime / duration);
			if (hp.fillAmount < cHp / fHp) break;
			yield return null;
		}
		hp.fillAmount = cHp / fHp;
		saveDamage = 0;
		fillCoroutine = null;
	}
}