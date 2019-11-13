using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : BulletPlayerSkill
{
    private Vector3 moveset = Vector3.right;
    public bool HitCheck = false;
    public WaterBallHit hit;
    public WaterPrison prison;
    private int skillid;
    private float damage;

    public void Setting(int id, float _damage)
    {
        skillid = id;
        damage = _damage;
        prison.transform.parent = GameMng.Ins.skillMng.transform;
    }

    public void SystemSetting(Vector3 pos)
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.transform.position = GameMng.Ins.player.transform.position;
        moveset = pos; 
        HitCheck = false; 
        hit.AttackCheck = true;
    }

    private void WaterHitSet()
    {
        if (prison.gameObject.activeSelf)
            return;

        if (HitCheck && !hit.AttackCheck)
        {
            prison.transform.position = gameObject.transform.position;
            prison.gameObject.SetActive(true);
            HitCheck = false;
            return;
        }
    }

    private void Update()
    {
        StartCoroutine(setBall());
        gameObject.transform.position += moveset * Time.deltaTime * 8.0f;
        if (!hit.AttackCheck) gameObject.SetActive(false);
        WaterHitSet();
    }
    private IEnumerator setBall()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }

    public override void Crash(Monster monster)
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
        moveset = Vector3.zero;
        HitCheck = true;
        hit.gameObject.SetActive(true);
        hit.Setting(skillid, gameObject.transform.position, damage);
    }
}
