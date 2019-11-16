using UnityEngine;

public class Aim : MonoBehaviour
{
	private int touchID;
	private Touch tempTouchs;
	public void SetTouchID(int _touchID)
	{
		touchID = _touchID;
		gameObject.SetActive(true);
		SetPos();
	}
	void Update()
	{
		SetPos();
	}
	private void SetPos()
	{
#if UNITY_EDITOR_WIN
		gameObject.transform.position = Input.mousePosition;
#else
				for (int i = 0; i < Input.touchCount; i++)
		{
			tempTouchs = Input.GetTouch(i);
			if (tempTouchs.fingerId == touchID)
			{
				gameObject.transform.position = tempTouchs.position;
				return;
			}
		}
#endif
	}
}
