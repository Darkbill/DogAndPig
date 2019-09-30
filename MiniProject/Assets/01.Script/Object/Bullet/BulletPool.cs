using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
	private const int iBulletCnt = 5;
	public GameObject bullet;
	private List<Bullet> bullets = new List<Bullet>();
	private void Start()
	{
		ResizeBullet(iBulletCnt);
	}
	public void OnBullet(Vector3 target, Vector3 startpos)
	{
		for (int i = 0; i < bullets.Count; ++i)
		{
			if(bullets[i].gameObject.activeSelf == false)
			{
				bullets[i].SetBulletStart(target, startpos);
				return;
			}
		}
		ResizeBullet(bullets.Count / 2);
		OnBullet(target, startpos);
	}
    //TODO : 몬스터나 플레이어나 둘다 쓸수있게끔 수정해범 ㅇㅅㅇ

	private void ResizeBullet(int addCount)
	{
		for (int i = 0; i < addCount; ++i)
		{
			GameObject bulletObject = Instantiate(bullet, gameObject.transform);
			bulletObject.SetActive(false);
			bullets.Add(bulletObject.GetComponent<Bullet>());
		}
	}
}
