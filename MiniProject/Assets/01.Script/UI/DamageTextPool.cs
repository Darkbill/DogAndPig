using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : MonoBehaviour
{
	public DamageText damageText;
	public List<DamageText> damageTextList = new List<DamageText>();
	public void ActiveDamageText(int damage,Vector3 pos)
	{
		for(int i = 0; i < damageTextList.Count; ++i)
		{
			if(damageTextList[i].gameObject.activeSelf == false)
			{
				damageTextList[i].ActiveDamageText(damage, pos);
				return;
			}
		}
		AddList();
		ActiveDamageText(damage, pos);
	}
	public void AddList()
	{
		for (int i = 0; i < damageTextList.Count / 2; ++i)
		{
			GameObject o = Instantiate(damageText.gameObject, gameObject.transform);
			damageTextList.Add(o.GetComponent<DamageText>());
		}
	}
}
