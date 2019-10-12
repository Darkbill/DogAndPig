using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : bullet host를 비교해서 
public class BulletBurn
{
    public Dictionary<eBulletType, BulletBase> stateDict = new Dictionary<eBulletType, BulletBase>();
    public BulletBase cState;

    public void Start()
    {
        stateDict.Add(eBulletType.None, new BulletHostNone());
        stateDict.Add(eBulletType.Player, new BulletHostPlayer());
        stateDict.Add(eBulletType.Monster, new BulletHostMonster());
        stateDict.Add(eBulletType.hostility, new BulletHosthostility());
        cState = stateDict[eBulletType.None];
    }

    public bool CallBulletBurn(eBulletType statetype, Vector3 pos)
    {
        cState = stateDict[statetype];
        return cState.BulletCheck(pos);
    }
}
public abstract class BulletBase
{
    public abstract bool BulletCheck(Vector3 pos);
}
public class BulletHostNone : BulletBase
{
    public override bool BulletCheck(Vector3 pos)
    {
        return false;
    }
}
public class BulletHostPlayer : BulletBase
{
    public override bool BulletCheck(Vector3 pos)
    {
        //테스트코드
        //if (Vector3.Distance(GameMng.Ins.monster.transform.position, pos) < 0.5f)
        //{
        //    return true;
        //}
        return false;
    }
}
public class BulletHostMonster : BulletBase
{
    public override bool BulletCheck(Vector3 pos)
    {
        if (Vector3.Distance(GameMng.Ins.player.transform.position, pos) < 0.5f)
        {
            return true;
        }
        return false;
    }
}
public class BulletHosthostility : BulletBase
{
    public override bool BulletCheck(Vector3 pos)
    {
        return false;
    }
}
