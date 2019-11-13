using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefine;
public class FireArrowGate : MonoBehaviour
{
	public ParticleSystem particle;
	public List<FireArrow> firArrowList;
	private float activeTime;
	private float arrowInitTime;
	private float damage;
	private float arrowSpeed;
	private float arrowActiveTime;
	private eAttackType skillType = eAttackType.Fire;
	public void Setting(float _activeTime, float _arrowInitTime, float _damage, float _arrowSpeed, float _arrowActiveTime)
	{
		activeTime = _activeTime;
		arrowInitTime = _arrowInitTime;
		damage = _damage;
		arrowSpeed = _arrowSpeed;
		arrowActiveTime = _arrowActiveTime;
		SettingArrow();
	}
	public void SettingArrow()
	{
		for (int i = 0; i < firArrowList.Count; ++i)
		{
			firArrowList[i].Setting(skillType, damage, arrowSpeed, arrowActiveTime);
		}
		gameObject.transform.parent = GameMng.Ins.skillMng.transform;
	}
	public void OffSkill()
	{
		for (int i = 0; i < firArrowList.Count; ++i)
		{
			firArrowList[i].gameObject.SetActive(false);
		}
		gameObject.SetActive(false);
	}
	public void StartGate()
	{
		gameObject.SetActive(true);
		gameObject.transform.position = GameMng.Ins.player.transform.position;
		gameObject.transform.eulerAngles = new Vector3(0, 0, GameMng.Ins.player.degree);
		particle.Play();
		StartCoroutine(CreateArrow());
	}

	private IEnumerator CreateArrow()
	{
		float activeDelay = 0;
		float initTime = 0;
		while (true)
		{
			activeDelay += Time.deltaTime;
			initTime += Time.deltaTime;
			Debug.Log(activeDelay);
			if (activeDelay >= activeTime)
			{
				OffSkill();
				yield break;
			}
			if(initTime >= arrowInitTime)
			{
				initTime = 0;
				SetArrow();
			}
			yield return null;
		}
	}
	private void SetArrow()
	{
		for (int i = 0; i < firArrowList.Count; ++i)
		{
			if (firArrowList[i].gameObject.activeSelf == false)
			{
				firArrowList[i].Setting(gameObject);
				break;
			}
		}
		FireArrow o = Instantiate(firArrowList[0], GameMng.Ins.skillMng.transform);
		firArrowList.Add(o);
		o.Setting(skillType, damage, arrowSpeed, arrowActiveTime);
		o.Setting(gameObject);
	}
}
