using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : MonoBehaviour
{
    public Alter Bullet;

    private float Range = 2f;
    const int Count = 10;

    private float timer = 0.0f;
    private float alltimer = 0.0f;

    private List<Alter> BulletLst = new List<Alter>();

    void Awake()
    {
        for (int i = 0; i < Count; ++i)
        {
            GameObject o = Instantiate(Bullet.gameObject, gameObject.transform);
            o.GetComponent<Alter>().Speed = i + 5;
            BulletLst.Add(o.GetComponent<Alter>());
        }
    }

    void Update()
    {
        //CheckMovVec();
        if (Input.GetKeyDown("q"))
        {
            CheckMovVec();
            //플레이어가 바라보는 방향으로 뛰쳐나가도록    
            GameMng.Ins.player.playerStateMachine.ChangeState(GlobalDefine.ePlayerState.Dash);
			GameMng.Ins.player.playerStateMachine.cState.isDash = true;

			for (int i = 0; i < Count; ++i)
            {
                BulletLst[i].Setting(GameMng.Ins.player.transform.position);
            }
        }
    }
    void CheckMovVec()
    {
        Vector3.Angle(GameMng.Ins.player.transform.right, new Vector3(0, 0, GameMng.Ins.player.transform.eulerAngles.z));
        for(int i = 0;i<Count;++i)
        {
            BulletLst[i].TargetPos = GameMng.Ins.player.transform.position + GameMng.Ins.player.transform.right * Range;
        }
    }
}
