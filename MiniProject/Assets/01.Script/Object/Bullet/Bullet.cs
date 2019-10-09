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

    private Vector3 StartPos;

    private float settimer = 0.0f;
    eBulletType bullettype = eBulletType.None;
    eBulletStyle bulletstyle = eBulletStyle.NORMAL;
    //Active True일 때 호출되는 유니티 함수
    BulletBurn bulletCheck = new BulletBurn();

    private int angle = 15;

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
	public void SetBulletStart(Vector3 target, Vector3 startpos, eBulletType host, eBulletStyle style)
	{
		gameObject.SetActive(true);
        StartPos = startpos;
        gameObject.transform.position = startpos;
        moveDirection = target - gameObject.transform.position;
        moveDirection.z = 0;
		moveDirection.Normalize();
        bullettype = host;
        bulletstyle = style;
        if (eBulletStyle.PARABOLARIGHT == style)
            angle = -15;
        else if(eBulletStyle.PARABOLARIGHT == style)
            angle = 15;
    }

    void BulletUpdate()
    {
        BulletStyle();
        gameObject.transform.position += moveDirection * Time.timeScale * 0.1f;
        BulletObjectCheck();

    }

    void BulletStyle()
    {
        switch(bulletstyle)
        {
            case eBulletStyle.BOOMERANG:
                BulletMoveStyleBoomerang();
                break;
            case eBulletStyle.BORN:
                break;
            case eBulletStyle.PARABOLARIGHT:
            case eBulletStyle.PARABOLALEFT:
                BulletMoveStyleParabola();
                break;
            case eBulletStyle.NONE:
                BulletMoveStyleNone();
                break;
        }
    }

    private void BulletMoveStyleParabola()
    {
        settimer += Time.deltaTime;
        if(settimer > 0.1f)
        {
            settimer = 0;
            var quater = Quaternion.Euler(0, 0, angle);
            moveDirection = quater * moveDirection;
        }
    }

    void BulletMoveStyleNone()
    {
        if (Vector3.Distance(StartPos, transform.position) < 0.1)
            gameObject.SetActive(false);
    }
    void BulletMoveStyleBoomerang()
    {
        settimer += Time.deltaTime;
        if(settimer > 0.5f)
        {
            settimer = 0.0f;
            moveDirection *= -1;
            bulletstyle = eBulletStyle.NONE;
            
        }
    }
    void BulletMoveStyleBorn()
    {

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
