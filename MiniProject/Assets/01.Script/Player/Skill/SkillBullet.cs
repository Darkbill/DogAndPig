using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet
{
    private float Speed = 5;

    public Vector3 BulletMovVec;
    public Vector3 BulletTarget;
    public FireBall BulletObject { get; set; }

    private Vector3 moveDirection;

    private BulletBurn bulletCheck = new BulletBurn();

    public void AwakeSet()
    {
        bulletCheck.Start();
    }

    public void BulletUpdate()
    {
        BulletObject.transform.position += moveDirection * Time.deltaTime * Speed;
    }
    public void UpdateTarget(Vector3 target)
    {
        moveDirection = target - BulletObject.transform.position;
    }

}
