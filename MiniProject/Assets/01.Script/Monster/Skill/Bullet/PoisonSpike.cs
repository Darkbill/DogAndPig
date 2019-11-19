using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpike : MonoBehaviour
{
    private int skillId;

    private float durationTime;
    private float damage;
    private float radius;
    private Quaternion degree;
    private float speed;
    private Vector3 startpos;
    private float addRange;

    private float setTime = 0.0f;

    private Monster objReference;

    public void Setting(int _id, float _durationtime, float _damage, float _radius, float _degree, float _speed)
    {
        skillId = _id;
        durationTime = _durationtime;
        damage = _damage;
        radius = _radius;
        degree = Quaternion.Euler(0, 0, 3);
        speed = _speed;
        gameObject.SetActive(false);

        //TODO : radius test

        radius = 2;
    }

    public void SystemSetting(float _degree, Monster _objRef)
    {
        setTime = 0.0f;
        addRange = 1.0f;
        if (_degree % 180 != 0)
            addRange = 1.2f;
        Vector3 right = Quaternion.Euler(0, 0, _degree) * Vector3.right * radius * addRange;
        objReference = _objRef;
        gameObject.transform.position = objReference.transform.position + right;
        gameObject.transform.right = right.normalized;
        gameObject.SetActive(true);
        startpos = gameObject.transform.position;
    }

    private void Update()
    {
        Vector3 right = degree * transform.right * radius * addRange;
        gameObject.transform.right = degree * transform.right;
        gameObject.transform.position = objReference.transform.position + right;
        setTime += Time.deltaTime;
        if (setTime > durationTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameMng.Ins.player.Damage(eAttackType.Water, damage);
            GameMng.Ins.HitToEffect(eAttackType.Water,
                        GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size),
                        gameObject.transform.position,
                        GameMng.Ins.player.calStat.size);
        }
    }
}
