﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : GoodBase
{

    //public override void BaseSetting(int id)
    //{
    //    base.BaseSetting(id);
    //}

    //public override void Running(Vector3 startpos, float range, int amnt)
    //{
    //    base.Running(startpos, range, amnt);
    //}

    public override void OnTriggetEntetObject()
    {
        GameMng.Ins.AddGold(amount);
        base.OnTriggetEntetObject();
    }
}
