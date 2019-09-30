using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// * private * //
    private const int iMapSizeExample = 10;
    private const int iBulletSpeed = 1;
    private const float fBulletPhysisSiz = 0.2f;
	private Vector3 moveDirection;

	// * protected * //
	protected int iHp;
	protected int iAtt;
	//TODO : Bullet Table 만들고 Time넣기
	protected float activeTime = 3;

	// * public * //
	public float fHorizontal { get; set; }
	public float fVertical { get; set; }
    eBulletType bullettype = eBulletType.None;
    //Active True일 때 호출되는 유니티 함수
    BulletBurn bulletCheck = new BulletBurn();

    private void Awake()
    {
        bulletCheck.Start();
    }
    private void OnEnable()
	{
		StartCoroutine(ActiveTimer());
    }
	void Update()
	{
		BulletUpdate();
	}
    //TODO : 몬스터 사용하려고 수정.
	public void SetBulletStart(Vector3 target, Vector3 startpos, eBulletType host)
	{
		gameObject.SetActive(true);
		gameObject.transform.position = startpos;
        moveDirection = target - gameObject.transform.position;
        moveDirection.z = 0;
		moveDirection.Normalize();
        bullettype = host;
    }

    void BulletUpdate()
    {
		gameObject.transform.position += moveDirection * Time.timeScale * 0.1f;
        BulletObjectCheck();

    }

    public void BulletObjectCheck()
    {
        if(bulletCheck.CallBulletBurn(bullettype, gameObject.transform.position))
        {
            OffBullet();
        }
    }

    //총알 끄는 시기에 총알마다 다른 효과를 주고싶으면 자식에서 재정의
    protected virtual void OffBullet()
	{
		gameObject.SetActive(false);
	}
	
	private IEnumerator ActiveTimer()
	{
		yield return new WaitForSeconds(activeTime);
		gameObject.SetActive(false);
	}
}
