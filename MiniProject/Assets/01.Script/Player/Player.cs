using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// * protected * //
	protected int iHp;
	protected int iAtt;

	// * public * //
	public float fHorizontal { get; set; }
	public float fVertical { get; set; }
	public Movement Mov;

	// * private * //
	private const int iBulletCnt = 20;
	protected int iSpeed;

    private void Awake()
    {
		PlayerSetting();
    }
	private void PlayerSetting()
	{
		iSpeed = 2;
	}
	private void Update()
	{
		PlayerMove();
	}


    void PlayerMove()
    {
        fHorizontal = Input.GetAxis("Horizontal");
        fVertical = Input.GetAxis("Vertical");

        Mov.iSpeed = iSpeed;
        transform.position += Mov.Move(fHorizontal, fVertical);
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
	}
}
