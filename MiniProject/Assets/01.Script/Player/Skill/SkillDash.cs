using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : MonoBehaviour
{
    public Alter Bullet;

    public float Range = 2f;
    const int Count = 10;

    private float timer = 0.0f;
    private float alltimer = 0.0f;

    private List<Alter> BulletLst = new List<Alter>();

    void Awake()
    {
        for (int i = 0; i < Count; ++i)
        {
            GameObject o = Instantiate(Bullet.gameObject, gameObject.transform);
            o.GetComponent<Alter>().Speed = i + 3;
            BulletLst.Add(o.GetComponent<Alter>());
        }
    }

    void Update()
    {
        CheckMovVec();
        if (Input.GetKeyDown("q"))
        {
            //플레이어가 바라보는 방향으로 뛰쳐나가도록    
            GameMng.Ins.player.playerStateMachine.ChangeState(GlobalDefine.ePlayerState.Dash);
            for (int i = 0; i < Count; ++i)
            {
                BulletLst[i].Setting(GameMng.Ins.player.transform.position);
            }
        }
    }
    void CheckMovVec()
    {
        Vector3.Angle(GameMng.Ins.player.transform.right, new Vector3());
        for(int i = 0;i<Count;++i)
        {
            BulletLst[i].TargetPos = GameMng.Ins.player.transform.position + GameMng.Ins.player.transform.right * Range;
        }
    }
}
