using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseObject
{
    const int iMapSizeExample = 10;
    const int iBulletSpeed = 1;
    const float fBulletPhysisSiz = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        iSpeed = iBulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        BulletUpdate();
    }

    void BulletUpdate()
    {
        gameObject.transform.position += new Vector3(
            (Time.deltaTime + fHorizontal) * iSpeed,
            (Time.deltaTime + fVertical) * iSpeed, 
            0);
        Vector3 pos = transform.position;

        if (pos.x > iMapSizeExample || pos.x < -iMapSizeExample ||
            pos.y > iMapSizeExample || pos.y < -iMapSizeExample)
            gameObject.SetActive(false);
    }
}
