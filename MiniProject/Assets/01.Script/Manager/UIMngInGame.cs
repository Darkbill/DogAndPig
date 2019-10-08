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
	public Image stickImage;
	public GameObject joyStick;
	public Vector3 stickPos;
	public float stickRadius = 60;
	public int moveTouchID;
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
		//stickImage.gameObject.transform.position = Input.mousePosition;
		//Vector3 dir = stickImage.gameObject.transform.position - stickPos;
		//float m = dir.magnitude;
		//dir.Normalize();
		//if (m > stickRadius)
		//{
		//	stickImage.gameObject.transform.position = stickPos + dir * stickRadius;
		//}
		//else
		//{
		//	stickImage.gameObject.transform.position = Input.mousePosition;
		//}
		//return dir;

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
	}
}
