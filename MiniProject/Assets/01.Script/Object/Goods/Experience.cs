using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : GoodBase
{
    public override void BaseSetting(int id)
    {
        base.BaseSetting(id);
    }

    public override void Running(Vector3 startpos, float range, int amnt)
    {
        base.Running(startpos, range, amnt);
    }

    public override void OnTriggetEntetObject()
    {
        GameMng.Ins.AddEXP(GameMng.stageLevel);
        //GameMng.Ins.AddGold(amount);
        base.OnTriggetEntetObject();
    }
}
