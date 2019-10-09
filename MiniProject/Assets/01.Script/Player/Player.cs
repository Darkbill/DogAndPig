using GlobalDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// * protected * //
	protected int iHp;
	protected int iAtt;
	protected int iSpeed;

	// * public * //
	public float fHorizontal { get; set; }
	public float fVertical { get; set; }
	public bool isMove;
	public Movement Mov;
	public int nowHealthPoint;
	public PlayerData playerData;

	// * private * //
	private const int iBulletCnt = 20;
	private int level = 1;


    //Gara;
    public GameObject localRotation;
    public GameObject AttImg;
	public SpriteRenderer playerSprite;

    //TODO : Att 범위랑 반지름 길이
    const float AttDegree = 30;
    const float AttRange = 0.7f;

    //360도
    const int Angle360 = 360;

    const int CriticalPers = 10;

    private void Awake()
	{
		PlayerSetting();
	}
	private void PlayerSetting()
	{
		iSpeed = 2;
		playerData = JsonMng.Ins.playerDataTable[level];
		nowHealthPoint = playerData.healthPoint;
	}
	private void Update()
	{
		if (isMove)
		{
			MoveToJoyStick();
		}
		else PlayerMove();

		if(Input.GetMouseButtonDown(0))
		{

		}
	}
	void PlayerMove()
	{
		fHorizontal = Input.GetAxis("Horizontal");
		fVertical = Input.GetAxis("Vertical");

        Mov.iSpeed = iSpeed;
        transform.position += Mov.Move(fHorizontal, fVertical);

        //TODO : 플레이어의 방향이랑 공격유무 시각적으로 표현하기위해 추가한 코드.
        //InGameCopy Scene에다가 확인가능.//AttDegree, AttRange
        Vector3 vMovement = new Vector3(0, 0, (Mathf.Atan2(fHorizontal, fVertical)) * Mathf.Rad2Deg * -1 - Angle360 / 4);
        //localRotation.transform.eulerAngles = vMovement;

        StartCoroutine(PlayerAtt());
    }

    IEnumerator PlayerAtt()
    {
        ObjectSetAttack att = new ObjectSetAttack();
        if (att.BaseAttack(new Vector3(fHorizontal, fVertical, 0),
                       GameMng.Ins.monster.transform.position - transform.position,
                       AttRange,
                       AttDegree))
        {
            //TODO : 몬스터 받아오는거는 여기에 list for문 굴려서 해당 객체에 대한 정보 받아오는 것으로 하면 될듯
            Debug.Log("플레이어 공격");
            if (Rand.Percent(CriticalPers))
                Debug.Log("Critical Hit");
            GameMng.Ins.monster.Hit();
            //AttImg.SetActive(true);
            yield return new WaitForSeconds(1);
        }
        else
        {
            //AttImg.SetActive(false);
        }
    }

    public void MoveToJoyStick()
	{
		Vector3 direction = UIMngInGame.Ins.GetJoyStickDirection();
		gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * iSpeed * Time.deltaTime;
	}

	public void Damage(int damage)
	{
		nowHealthPoint -= damage;
	}
}
