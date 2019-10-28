using UnityEngine;
using UnityEngine.UI;
public class MonsterExam : MonoBehaviour
{
	public Text levelText;
	public Text indexText;
	public int boss;
	public bool isDrag;
	public void Setting(string level,string index,float xPos,float yPos,int b)
	{
		gameObject.SetActive(true);
		levelText.text = level;
		indexText.text = index;
		Rect r = transform.parent.GetComponent<RectTransform>().rect;
		gameObject.transform.localPosition = new Vector3((r.width/12)*xPos, (r.height/6)*yPos, 0);
		boss = b;
		if(boss == 1)
		{
			gameObject.GetComponent<Image>().color = Color.red;
		}
	}
	public Vector2 GetCoord()
	{
		Rect r = transform.parent.GetComponent<RectTransform>().rect;
		return new Vector2(gameObject.transform.localPosition.x / (r.width / 12), gameObject.transform.localPosition.y / (r.height / 6));
	}
	private void Update()
	{
		if(isDrag)
		{
			gameObject.transform.position = Input.mousePosition;
		}
	}
	public void OnDrag()
	{
		isDrag = true;
	}
	public void OnDrop()
	{
		isDrag = false;
	}
}
