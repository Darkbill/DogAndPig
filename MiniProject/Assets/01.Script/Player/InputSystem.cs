using GlobalDefine;
using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
            //TODO : 원형탄 테스트 circleshot 매개변수 10은 탄환개수임!!
            BulletPattern monsterpat = new BulletPlayer();
            monsterpat.SettingPos(Camera.main.ScreenToViewportPoint(Input.mousePosition),
                                     GameMng.Ins.player.transform.position, eBulletType.Player);
            CircleShot Att01 = new CircleShot(monsterpat, 10);
            Att01.BulletShot();
			UIMngInGame.Ins.OnStickDrop();
		}
		if(Input.GetMouseButtonDown(0))
		{
			UIMngInGame.Ins.OnStrickDrag();
		}
	}
}
