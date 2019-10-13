using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalDefine
{
	static public class Define
	{
		public const int nRANDOM_POOL = 1024;
	}

	public enum eBulletStyle
    {
        NONE = 0,
        NORMAL,
        BOOMERANG,
        PARABOLALEFT,
        PARABOLARIGHT,
        BORN,
		Max,
    }
	public enum eBulletType
    {
        None = 0,
        Player,
        Monster,
        hostility,
		Max,
    }
	public enum eMilliMonsterState
	{
		None = 0,
		Idle,
		Move,
		Stun,
		SkillAttack,
		Dash,
		Dead,
		Max,
	}
	public enum eMonsterMoveType
	{
		None = 0,
		Move,
		NotMove,
	}
	public enum eMonsterAttackType
	{
		None = 0,
		Milli,
		Range,
	}
	public enum eRangeMonsterState
	{
		None = 0,
		Idle,
		Move,
		Attack,
		Stun,
		SkillAttack,
		Dash,
		Dead,
		Max,
	}
	public enum ePlayerState
	{
		None = 0,
		Idle,
		Move,
		Dash,
		Stun,
		Dead,
		Max,
	}
	public enum eAttackType
	{
		None = 0,
		Physics,
		Fire,
		Water,
		Wind,
		Lightning,
		Max,
	}
	public enum eWallType
	{
		None = 0,
		Up,
		Down,
		Right,
		Left,
		Max,
	}
	public enum eSkillType
	{
		None = 0,
		Other,
		Self,
	}

	static public class Rand // 만분율 기준 0~9999까지 저장
	{
		private static int Index = 0;
		private static int[] randomArr = new int[Define.nRANDOM_POOL];

		static Rand()
		{
			for (int i = 0; i < Define.nRANDOM_POOL; ++i)
			{
				randomArr[i] = UnityEngine.Random.Range(0, 10000);
			}
		}

		private static int nIndex
		{
			get
			{
				int nTemp = Index++;

				if (Index >= Define.nRANDOM_POOL) { Index = 0; }

				return nTemp;
			}
		}

		public static int Random() { return randomArr[nIndex]; }

		public static bool Percent(float a_nPercent) { return randomArr[nIndex] <= (a_nPercent * 100); }
		public static bool Permile(int a_nPermile) { return randomArr[nIndex] <= (a_nPermile * 10); }
		public static bool Permilad(int a_nPermilad) { return randomArr[nIndex] <= a_nPermilad; }
		public static int Range(int a_nStart, int a_nEnd)
		{
			if (a_nStart > a_nEnd)
			{
				int nTemp = a_nStart;
				a_nStart = a_nEnd;
				a_nEnd = nTemp;
			}

			return (Random() % (a_nEnd - a_nStart)) + a_nStart;
		}
	}
	public interface IBattle
	{
		void Attack(eAttackType attackType, float damage);
		void Damage(eAttackType attackType, float damage);
	}
}