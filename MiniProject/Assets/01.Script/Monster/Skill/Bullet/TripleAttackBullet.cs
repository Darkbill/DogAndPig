using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleAttackBullet : MonoBehaviour
{

    private int skillId;

    private float damage;
    private float speed;
    private float endTime;
    private float setTime;

    public void Setting(int _id, float _damage, float _speed, float _endTime)
    {
        skillId = _id;

        damage = _damage;
        speed = _speed;
        endTime = _endTime;
    }

    public void SystemSetting(float _angle, Vector3 pos)
    {
        gameObject.transform.right = Quaternion.Euler(0, 0, _angle) * Vector3.right;
        gameObject.transform.position = pos;
        setTime = 0.0f;
        gameObject.SetActive(true);
    }

    public void EndSetting()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        setTime += Time.deltaTime;
        if (setTime > endTime)
            gameObject.SetActive(false);

        gameObject.transform.position += gameObject.transform.right * Time.deltaTime * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //PlayerHit
            GameMng.Ins.player.Damage(eAttackType.Wind, damage);
            GameMng.Ins.HitToEffect(eAttackType.Wind,
                GameMng.Ins.player.transform.position + new Vector3(0, GameMng.Ins.player.calStat.size),
                gameObject.transform.position,
                GameMng.Ins.player.calStat.size);
        }
    }
}
