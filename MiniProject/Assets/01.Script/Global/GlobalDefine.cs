using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalDefine
{
    public enum eBulletType
    {
        None = 0,
        Player,
        Monster,
        hostility,
    }
	public enum eMonsterState
	{
		None = 0,
		Idle,
		Chase,
		Attack,
		Dodge,
		Dead,
	}
	public enum eSpawnWay
	{
		None = 0,
		Top,
		Bottom,
		Right,
		Left,
		All,
	}
}