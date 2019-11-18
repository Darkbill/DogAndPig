using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : MonoBehaviour
{
    private const float stopTime = 0.2f;

    private int skillId;

    private float durationTime;
    private float setTime;
    private float damage;
    private float radius;
    private bool stopCheck;

    public ParticleSystem mucusEffect;

    public void Setting(int _id, float _time, float _damage, float _radius)
    {
        skillId = _id;
        radius = _radius;

        durationTime = 5;// _time;
        damage = _damage;
        gameObject.SetActive(false);
        gameObject.transform.parent = GameMng.Ins.skillMng.transform;

        //TODO : radian 기본 값 1. 테스트
        radius = 1;

    }


    public void SystemSetting(Vector3 _pos)
    {
        setTime = 0.0f;
        gameObject.transform.position = _pos;
        gameObject.SetActive(true);
        stopCheck = false;
        mucusEffect.Play();
    }

    void Update()
    {
        setTime += Time.deltaTime;
        if(setTime > stopTime && !stopCheck)
        {
            stopCheck = true;
            mucusEffect.Pause();
        }
        if(setTime > durationTime) mucusEffect.Play();
        if (setTime > durationTime + 1.0f)
        {
            mucusEffect.Stop();
            gameObject.SetActive(false);
        }

        CircleInfoCheck();
    }

    private void CircleInfoCheck()
    {
        if((gameObject.transform.position - GameMng.Ins.player.transform.position).magnitude < radius)
        {
            GameMng.Ins.player.AddBuff(new ConditionData(eBuffType.MoveSlow, skillId, 3, 1));
        }
    }
}
