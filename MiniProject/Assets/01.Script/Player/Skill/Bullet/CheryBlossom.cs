using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheryBlossom : BulletPlayerSkill
{
    private List<ParticleSystem> clossomSystem = new List<ParticleSystem>();

    private float delayTime;


    //base - movespeed up
    //add - collider attack(dash
    //add - collider

    public void Setting()
    {
    }
    public void SystemSetting()
    {
        //애니메이션 재생
        //이동속도 증가 버프
        //collider enable true
    }
    private void Update()
    {

    }
    public override void Crash(Monster monster)
    {
        
    }
}
