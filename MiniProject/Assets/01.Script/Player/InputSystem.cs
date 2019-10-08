using GlobalDefine;
using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
	private Touch tempTouchs;
	private bool isMove = false;
	private void Update()
	{
#if UNITY_EDITOR_WIN
		/* 컴퓨터 빌드 */
		if (Input.GetMouseButtonUp(0))
		{
			//TODO : 원형탄 테스트 circleshot 매개변수 10은 탄환개수임!!

			BulletPattern monsterpat = new BulletPlayer();
			monsterpat.SettingPos(Camera.main.ScreenToWorldPoint(Input.mousePosition),
										GameMng.Ins.player.transform.position, eBulletType.Player);
			CircleShot Att01 = new CircleShot(monsterpat, 10);
			Att01.BulletShot();


			UIMngInGame.Ins.OnStickDrop();

		}
		if (Input.GetMouseButtonDown(0))
		{
			//UIMngInGame.Ins.OnStrickDrag();
		}
#else

		/* 모바일 빌드 */
		if (Input.touchCount > 0)
		{    //터치가 1개 이상이면.
			for (int i = 0; i < Input.touchCount; i++)
			{
				tempTouchs = Input.GetTouch(i);
				if (tempTouchs.phase == TouchPhase.Ended)
				{
					if (tempTouchs.fingerId == UIMngInGame.Ins.moveTouchID)
					{
						UIMngInGame.Ins.OnStickDrop();
					}
					else
					{
						BulletPattern monsterpat = new BulletPlayer();
						monsterpat.SettingPos(Camera.main.ScreenToViewportPoint(Input.mousePosition),
													GameMng.Ins.player.transform.position, eBulletType.Player);
						CircleShot Att01 = new CircleShot(monsterpat, 10);
						Att01.BulletShot();
					}
				}
				else if (tempTouchs.phase == TouchPhase.Moved)
				{
					if (UIMngInGame.Ins.moveTouchID != -1) continue;
					UIMngInGame.Ins.OnStrickDrag(tempTouchs);
				}
			}
		}
#endif
	}
	private void FixedUpdate()
	{

	}
}
