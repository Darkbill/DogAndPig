using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObject
{
    List<GameObject> objBullets = new List<GameObject>();

    public GameObject objBullet;
    public Movement Mov;

    private const int iBulletCnt = 20;

    // Start is called before the first frame update
    void Awake()
    {
        iSpeed = 10;
        for (int i = 0; i < iBulletCnt; ++i)
        {
            GameObject bullet = Instantiate(objBullet);
            bullet.SetActive(false);
            objBullets.Add(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        fHorizontal = Input.GetAxis("Horizontal");
        fVertical = Input.GetAxis("Vertical");

        //Debug.Log("(" + fHorizontal + " : " + fVertical + ")");

        Mov.iSpeed = iSpeed;
        transform.position += Mov.Move(fHorizontal, fVertical);

        if(Input.GetKeyDown("z"))
        {
            BulletSetActivate();
        }
    }

    void BulletSetActivate()
    {
        for (int i = 0; i < iBulletCnt; ++i)
        {
            if(!objBullets[i].activeSelf)
            {
                objBullets[i].SetActive(true);
                objBullets[i].GetComponent<BaseObject>().SetObjectStartPos(transform.position);
                objBullets[i].GetComponent<BaseObject>().fHorizontal = fHorizontal;
                objBullets[i].GetComponent<BaseObject>().fVertical = fVertical;
                break;
            }
        }
    }
}
