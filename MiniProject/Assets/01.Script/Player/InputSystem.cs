using UnityEngine;
public class InputSystem : MonoBehaviour
{
	private Touch tempTouchs;
	private int touchID = -1;
    private void Update()
	{
#if UNITY_EDITOR_WIN
		/* 컴퓨터 빌드 */
		if (Input.GetMouseButtonDown(0))
		{
			UIMngInGame.Ins.OnStrickDrag();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			UIMngInGame.Ins.OnStickDrop();
		}
#else
		/* 모바일 빌드 */
		if (Input.touchCount > 0)
		{    
			for (int i = 0; i < Input.touchCount; i++)
			{
				tempTouchs = Input.GetTouch(i);
				if (tempTouchs.phase == TouchPhase.Ended)
				{
					if (tempTouchs.fingerId == UIMngInGame.Ins.moveTouchID)
					{
						UIMngInGame.Ins.OnStickDrop();
						touchID = -1;
					}
				}
				else if (tempTouchs.phase == TouchPhase.Began)
				{
					if (touchID == -1)
					{
						UIMngInGame.Ins.OnStrickDrag();
						touchID = tempTouchs.fingerId;
					}
				}
			}
		}
#endif
	}
}