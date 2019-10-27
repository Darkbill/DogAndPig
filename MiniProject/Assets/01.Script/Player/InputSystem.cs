using UnityEngine;
public class InputSystem : MonoBehaviour
{
	private Touch tempTouchs;
	private int touchID = -1;
	private int skillID = -1;
	public bool isSkillDrag;


	private void Update()
	{
		/* 컴퓨터 빌드 */
		if (Input.GetMouseButtonDown(0))
		{
			if (isSkillDrag == true)
			{
				GameMng.Ins.StartSkillAim();
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
					else if (tempTouchs.fingerId == skillID)
					{
						UIMngInGame.Ins.OnSkillDrop();
						skillID = -1;
					}
				}
				else if (tempTouchs.phase == TouchPhase.Began)
				{
					if (isSkillDrag == true)
					{
						UIMngInGame.Ins.OnSkillDrag(tempTouchs.fingerId);
						skillID = tempTouchs.fingerId;
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

	}
}