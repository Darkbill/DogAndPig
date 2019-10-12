using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : Skill
{
	enum eDashOption
	{
		Damage,
		coolTime,
	}
	private float damage;
	private float coolTime;
	public override void SkillSetting()
	{
		skillID = 1;
		PlayerSkillData skillData = JsonMng.Ins.playerSkillDataTable[skillID];
		skillType = skillData.skillType;
		target = skillData.target;
		damage = skillData.optionArr[(int)eDashOption.Damage];
		damage = skillData.optionArr[(int)eDashOption.Damage];
	}


	public Alter Bullet;
    private const float Range = 2f;
    private const int Count = 10;
    private List<Alter> alterList = new List<Alter>();


	void Start()
    {
        for (int i = 0; i < Count; ++i)
        {
            GameObject o = Instantiate(Bullet.gameObject, gameObject.transform);
            o.GetComponent<Alter>().Speed = i + 5;
			alterList.Add(o.GetComponent<Alter>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            CheckMovVec();
            ChangeSet();
        }
    }

    public void ChangeSet()
    {
        //플레이어가 바라보는 방향으로 뛰쳐나가도록    
        GameMng.Ins.player.playerStateMachine.ChangeState(GlobalDefine.ePlayerState.Dash);
        GameMng.Ins.player.playerStateMachine.cState.isDash = true;

        for (int i = 0; i < Count; ++i)
        {
			alterList[i].Setting(GameMng.Ins.player.transform.position);
        }
    }

    void CheckMovVec()
    {
        Vector3.Angle(GameMng.Ins.player.transform.right, new Vector3(0, 0, GameMng.Ins.player.transform.eulerAngles.z));
        for(int i = 0;i<Count;++i)
        {
			alterList[i].TargetPos = GameMng.Ins.player.transform.position + GameMng.Ins.player.transform.right * Range;
        }
    }
}
