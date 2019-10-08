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

	public void OnStrickDrag()
	{
		GameMng.Ins.player.isMove = true;
		joyStick.gameObject.SetActive(true);
		joyStick.gameObject.transform.position = Input.mousePosition;
		stickPos = stickImage.gameObject.transform.position;
	}
	public void OnStickDrop()
	{
		GameMng.Ins.player.isMove = false;
		joyStick.gameObject.SetActive(false);
		stickImage.gameObject.transform.position = stickPos;
	}
	public Vector3 GetJoyStickDirection()
	{
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
	}
}
