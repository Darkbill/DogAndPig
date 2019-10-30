using UnityEngine;
using GlobalDefine;
public class Arrow : MonoBehaviour
{
	public Rigidbody2D rig;
	private float damage;
	public void Setting(Vector3 startPos, Vector3 dir,float _damage)
	{
		gameObject.SetActive(true);
		gameObject.transform.position = new Vector3(startPos.x, startPos.y, 0);
		gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 1);
		gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
		gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 300);
		damage = _damage;
	}
	private void Update()
	{
		gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rig.velocity.y, rig.velocity.x) * Mathf.Rad2Deg);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			GameMng.Ins.player.Damage(eAttackType.Physics, damage);
			gameObject.SetActive(false);
		}
		else if(collision.CompareTag("Wall"))
		{
			gameObject.SetActive(false);
		}
	}
}
