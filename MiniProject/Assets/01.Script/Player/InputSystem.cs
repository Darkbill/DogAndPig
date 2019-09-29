using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			GameMng.Ins.bulletPool.OnBullet(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                GameMng.Ins.player.transform.position);
		}
	}
}
