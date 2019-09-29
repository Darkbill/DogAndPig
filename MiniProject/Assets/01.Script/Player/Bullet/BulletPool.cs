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
	public void OnBullet()
	{
		for (int i = 0; i < bullets.Count; ++i)
		{
			if(bullets[i].gameObject.activeSelf == false)
			{
				bullets[i].SetBulletStart();
				return;
			}
		}
		ResizeBullet(bullets.Count / 2);
		OnBullet();
	}
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
