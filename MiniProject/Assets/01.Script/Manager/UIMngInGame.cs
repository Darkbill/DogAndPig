using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMngInGame : MonoBehaviour
{
	#region SINGLETON
	static UIMngInGame _instance = null;

	public static UIMngInGame Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UIMngInGame)) as UIMngInGame;
				if (_instance == null)
				{
					_instance = new GameObject("UIMngInGame", typeof(UIMngInGame)).GetComponent<UIMngInGame>();
				}
			}

			return _instance;
		}
	}
	#endregion
	/* Player Controller UI */
	public Image stickImage;
	public GameObject joyStick;
	public Vector3 stickPos;
	public float stickRadius = 60;
	public int moveTouchID;

	/* Player Infomation UI*/
	public Image healthGageImage;
	public Text healthText;
	public Coroutine fillCoroutine;
	public float saveDamage = 0;
	private void Start()
	{
		UISetting();
	}
	public void UISetting()
	{
		healthText.text = string.Format("{0} / {1} ", GameMng.Ins.player.nowHealthPoint, GameMng.Ins.player.playerData.healthPoint);
	}
	public void OnStrickDrag()
	{
		GameMng.Ins.player.isMove = true;
		joyStick.gameObject.SetActive(true);
		joyStick.gameObject.transform.position = Input.mousePosition;
		stickPos = stickImage.gameObject.transform.position;
	}
	public void OnStrickDrag(Touch touch)
	{
		GameMng.Ins.player.isMove = true;
		joyStick.gameObject.SetActive(true);
		moveTouchID = touch.fingerId;
		joyStick.gameObject.transform.position = touch.position;
		stickPos = stickImage.gameObject.transform.position;
	}
	public void OnStickDrop()
	{
		GameMng.Ins.player.isMove = false;
		joyStick.gameObject.SetActive(false);
		moveTouchID = -1;
	}
	public Vector3 GetJoyStickDirection()
	{
		/* 컴퓨터 빌드 */
#if UNITY_EDITOR_WIN
		stickImage.gameObject.transform.position = Input.mousePosition;
		Vector3 dir = stickImage.gameObject.transform.position - stickPos;
		float m = dir.magnitude;
		dir.Normalize();
		if (m > stickRadius)
		{
			stickImage.gameObject.transform.position = stickPos + dir * stickRadius;
		}
		else
		{
			stickImage.gameObject.transform.position = Input.mousePosition;
		}
		return dir;
#else
		/* 모바일 빌드 */
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch tempTouchs = Input.GetTouch(i);

			if (tempTouchs.fingerId == moveTouchID)
			{
				stickImage.gameObject.transform.position = tempTouchs.position;
				Vector3 dir = stickImage.gameObject.transform.position - stickPos;
				float m = dir.magnitude;
				dir.Normalize();
				if (m > stickRadius)
				{
					stickImage.gameObject.transform.position = stickPos + dir * stickRadius;
				}
				else
				{
					stickImage.gameObject.transform.position = tempTouchs.position;
				}
				return dir;
			}
		}
		return Vector3.zero;
#endif
	}
	public void DamageToPlayer(int damage)
	{
		if (fillCoroutine != null)
		{
			StopCoroutine(fillCoroutine);
		}
		fillCoroutine = StartCoroutine(IEDamageToPlayer(damage, saveDamage, 0.5f));
		healthText.text = string.Format("{0} / {1} ", GameMng.Ins.player.nowHealthPoint, GameMng.Ins.player.playerData.healthPoint);
	}
	IEnumerator IEDamageToPlayer(int damage, float save, float duration)
	{
		saveDamage = damage + save;
		float cTime = 0;
		//전체 체력대비 깍아야하는 체력의 비율
		float minus = saveDamage / GameMng.Ins.player.playerData.healthPoint;
		while (cTime < duration)
		{
			cTime += Time.deltaTime;
			saveDamage -= saveDamage * (Time.deltaTime / duration);
			//현채 fill에서 추가로 깎는다 ~초 까지
			healthGageImage.fillAmount -= minus * (Time.deltaTime / duration);
			Debug.Log(healthGageImage.fillAmount);
			if (healthGageImage.fillAmount < GameMng.Ins.player.nowHealthPoint / GameMng.Ins.player.playerData.healthPoint) break;
			yield return null;
		}
		healthGageImage.fillAmount = (float)GameMng.Ins.player.nowHealthPoint / GameMng.Ins.player.playerData.healthPoint;
		saveDamage = 0;
		fillCoroutine = null;
	}
}
