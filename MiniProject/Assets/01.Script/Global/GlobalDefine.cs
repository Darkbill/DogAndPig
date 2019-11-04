namespace GlobalDefine
{
    static class DefineClass
    {
        public const float MapSizX = 60;
        public const float MapSizY = 35;
    }
	static public class Define
	{
		public const int nRANDOM_POOL = 1024;
		public const float coolDownTimeAll = 0.3f;
		public const float upscaleDuration = 2f;
		public const float standardMoveSpeed = 0.625f;
		public const float standardAttackSpeed = 1;
		public const int lobbyStartCount = 3;
		public const int restartCount = 3;
		public const int adCount = 3;
		public const float knockBackSpeed = 10;
	}

    public enum eBossMonsterState
    {
        None = 0,
        Idle,
        Move,
        Stun,
        SkillAttack,
        Dash,
        Dead,
        KnockBack,
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
	public enum eMonsterState
	{
		None = 0,
		Idle,
		Move,
		Damage,
		Attack,
		SkillAttack,
		Dash,
		Stun,
		KnockBack,
		Dead,
		Max,
	}
	public enum ePlayerState
	{
		None = 0,
		Idle,
		Move,
		Dash,
        KnockBack,
		Stun,
		Dead,
		Max,
	}
	public enum eSkillActiveType
	{
		None = 0,
		Buff,
		Active,
		Target,
	}
	public enum ePlayerAnimation
	{
		Idle = 0,
		Run,
		Damage,
		Attack,
		Dead,
	}

    public enum eMonsterAnimation
    {
        Idle = 0,
        Run,
        Damage,
        Attack,
        Skill,
        Dead,
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
	public enum eBuffType
	{
		None = 0,
		MoveFast,
		MoveSlow,
		PhysicsStrong,
		PhysicsWeek,
        NockBack,
		Stun,
		Max,
	}
	public enum eSkillType
	{
		None = 0,
		Other,
		Self,
	}
	public enum eBoxType
	{
		Buy = 0,
		Set,
		Remove,
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
		public static bool Permile(float a_nPermile) { return randomArr[nIndex] <= (a_nPermile * 10); }
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
}