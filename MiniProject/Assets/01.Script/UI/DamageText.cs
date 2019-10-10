using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
	public Text text;
	public void ActiveDamageText(int damage,Vector3 pos)
	{
		gameObject.SetActive(true);
		text.text = damage.ToString();
		gameObject.transform.position = pos;
		gameObject.transform.DOMoveY(gameObject.transform.position.y + 100, 1f).OnComplete(() => { gameObject.SetActive(false); });
	}
}
