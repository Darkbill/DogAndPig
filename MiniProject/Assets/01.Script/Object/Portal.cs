using UnityEngine;

public class Portal : MonoBehaviour
{
	public CircleCollider2D col;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		UIMngInGame.Ins.Fade(true);
		gameObject.SetActive(false);
	}
}
