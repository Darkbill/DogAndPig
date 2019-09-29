using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			GameMng.Ins.bulletPool.OnBullet();
		}
	}
}
