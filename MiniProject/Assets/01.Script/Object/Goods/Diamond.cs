﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : GoodBase
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
        GameMng.Ins.AddDiamond(amount);
        base.OnTriggetEntetObject();
    }
}
