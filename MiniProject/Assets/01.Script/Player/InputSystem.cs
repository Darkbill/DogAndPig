using GlobalDefine;
using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
    BulletPattern bulletpattern = new BulletPattern(eBulletType.Player);

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
            //TODO : 원형탄 테스트 circleshot 매개변수 10은 탄환개수임!!
            bulletpattern.SettingPos(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                                     GameMng.Ins.player.transform.position);
            bulletpattern.CircleShot(10);
		}
	}
}
