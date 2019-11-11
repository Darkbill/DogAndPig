using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class Alter : BulletPlayerSkill
{
    public float Speed;
	private const float Range = 2f;
	public Vector3 TargetPos = new Vector3();
    private Vector3 setplayerpos = new Vector3(0.01f, 0.25f, 0);

    public bool StartMove;
    public List<Monster> hitMonsterList = new List<Monster>();
    public void Setting(Vector3 startPos,Vector3 direction, float speed)
    {
        gameObject.SetActive(true);
        StartMove = true;
        gameObject.transform.position = startPos;
		TargetPos = startPos + direction * Range;
		Speed = speed;
        hitMonsterList.Clear();
    }
    void Update()
    {
        if(StartMove)
        {
            Vector3 movement = TargetPos - gameObject.transform.position;
            movement.z = 0;
            if(Vector3.Distance(transform.position, TargetPos) < 0.2f ||
                Vector3.Distance(transform.position, GameMng.Ins.player.transform.position + setplayerpos) < 0.2f)
            {
                gameObject.SetActive(false);
                StartMove = false;
            }
            else
            {
                gameObject.transform.position += movement * Time.deltaTime * Speed;
            }
        }
    }

    public override void Crash(Monster monster)
    {
        hitMonsterList.Add(monster);
        hitMonsterList = hitMonsterList.Distinct().ToList();
    }
}
