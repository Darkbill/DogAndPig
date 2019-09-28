﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseObject
{
    const int iMapSizeExample = 10;
    const int iBulletSpeed = 1;
    const float fBulletPhysisSiz = 0.2f;

    void Start()
    {
        iSpeed = iBulletSpeed;
    }

    void Update()
    {
        BulletUpdate();
    }

    void BulletUpdate()
    {
        gameObject.transform.position += new Vector3(
            (Time.deltaTime + fHorizontal * fBulletPhysisSiz) * iSpeed,
            (Time.deltaTime + fVertical * fBulletPhysisSiz) * iSpeed, 
            0);
        Vector3 pos = transform.position;

        if (pos.x > iMapSizeExample || pos.x < -iMapSizeExample ||
            pos.y > iMapSizeExample || pos.y < -iMapSizeExample)
            gameObject.SetActive(false);
    }
}
