using UnityEngine;
using GlobalDefine;
public class SwordDance : MonoBehaviour
{
	private float damage;
	private float speed;
	private const float durationTime = 5f;
	private float cTime;
	private Vector3 direction;
	public void Setting(float _damage,float _speed)
	{
		damage = _damage;
		speed = _speed;
		gameObject.transform.parent = GameMng.Ins.objectPool.transform;
	}
	public void Shot(Vector3 dir, Vector3 startPos,float size,float degree,bool isLeft,bool lookRight)
	{
		//날아가는 방향, 시작 위치, 몬스터사이즈에 따른 위치 조정
		gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
		gameObject.transform.position = startPos;

		if (lookRight) gameObject.transform.position = startPos + new Vector3(size, size, 0);
		else gameObject.transform.position = startPos + new Vector3(-size, size, 0);

		direction = dir.normalized;
		cTime = 0;
		gameObject.SetActive(true);
	}
	private void Update()
	{
		gameObject.transform.position += direction * speed * Time.deltaTime;
		cTime += Time.deltaTime;
		if(cTime >= durationTime)
		{
			gameObject.SetActive(false);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			GameMng.Ins.DamageToPlayer(eAttackType.Physics, damage);
			gameObject.SetActive(false);
		}
	}
}
