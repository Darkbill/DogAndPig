using UnityEngine;
public class InputSystem : MonoBehaviour
{
	private Touch tempTouchs;
	public int touchID = -1;
	private int skillTouchID = -1;
	//윈도우 빌드용
	public bool isSkillDrag;

	private void Update()
	{
		/* 컴퓨터 빌드 */
#if UNITY_EDITOR_WIN
		if (Input.GetMouseButtonDown(0))
		{
			if (isSkillDrag)
			{
				UIMngInGame.Ins.OnSkillDrag(0);
				isSkillDrag = false;
			}
		}
        if(Input.GetKeyDown("1"))
        {
            UIMngInGame.Ins.StartSkillSet(0);
        }
        if(Input.GetKeyDown("2"))
        {
            UIMngInGame.Ins.StartSkillSet(1);
        }
        if (Input.GetKeyDown("3"))
        {
            UIMngInGame.Ins.StartSkillSet(2);
        }
        if (Input.GetKeyDown("4"))
        {
            UIMngInGame.Ins.StartSkillSet(3);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIMngInGame.Ins.InerruptGame(0);
            UIMngInGame.Ins.InerruptGameSet();
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
					if (tempTouchs.fingerId == touchID)
					{
						UIMngInGame.Ins.OnStickDrop();
						touchID = -1;
					}
					else if (tempTouchs.fingerId == skillTouchID)
					{
						GameMng.Ins.EndSkillAim(tempTouchs.position);
						skillTouchID = -1;
					}
				}
				else if (tempTouchs.phase == TouchPhase.Began)
				{
					if (isSkillDrag)
					{
						UIMngInGame.Ins.OnSkillDrag(tempTouchs.fingerId);
						skillTouchID = tempTouchs.fingerId;
						isSkillDrag = false;
					}
					else if (touchID == -1)
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