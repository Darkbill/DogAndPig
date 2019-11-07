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
		public const float knockBackSpeed = 5;
		public const int inventoryCount = 9;
		public static eItemGradeType GetGrade()
		{
			int per = Rand.Range(0, 1000);
			if (per <= 700)
			{
				return eItemGradeType.Normal;
			}
			else if (per <= 900)
			{
				return eItemGradeType.Rare;
			}
			else if (per <= 990)
			{
				return eItemGradeType.Unique;
			}
			else
			{
				return eItemGradeType.Legend;
			}
		}
		public static float GetChangeValue(eItemGradeType type)
		{
			switch (type)
			{
				case eItemGradeType.Normal:
					return UnityEngine.Random.Range(0.1f, 0.3f);
				case eItemGradeType.Rare:
					return UnityEngine.Random.Range(0.3f, 0.4f);
				case eItemGradeType.Unique:
					return UnityEngine.Random.Range(0.4f, 0.6f);
				case eItemGradeType.Legend:
					return UnityEngine.Random.Range(0.6f, 0.7f);
				default:
					return 0;
			}
		}
		public static string GetPartString(eUpgradeType type)
		{
			switch (type)
			{
				case eUpgradeType.Damage:
					return "데미지";
				case eUpgradeType.Armor:
					return "방어력";
				case eUpgradeType.CriticalChance:
					return "크리티컬 확률";
				case eUpgradeType.CriticalDamage:
					return "크리티컬 데미지";
				case eUpgradeType.FireResist:
					return "화염 저항";
				case eUpgradeType.HP:
					return "체력";
				case eUpgradeType.KnockBack:
					return "넉백 확률";
				case eUpgradeType.PhysicsResist:
					return "물리 저항";
				case eUpgradeType.Speed:
					return "이동 속도";
				case eUpgradeType.WaterResist:
					return "물 저항";
				case eUpgradeType.WindResist:
					return "바람 저항";
				default:
					return "";
			}
		}
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
		Equip,
		EquitTakeOff,
	}
	public enum eItemType
	{
		Head,
		Neck,
		Hand,
		Ring,
	}
	public enum eItemGradeType
	{
		None = 0,
		Normal,
		Rare,
		Unique,
		Legend,
		Max,
	}
	public enum eUpgradeType
	{
		None = 0,
		HP,
		Damage,
		Speed,
		CriticalChance,
		CriticalDamage,
		Armor,
		KnockBack,
		PhysicsResist,
		FireResist,
		WaterResist,
		WindResist,
	}

	/// <summary>
	/// 테스트
	/// </summary>
	public enum eSkillOption
	{
		None,
		Damage,
		CoolTime,
		ActiveTime,
		Speed,
		SpawnDelay,
		SpawnActveTime,
		BuffActivePer,
		BuffEndTime,
		BuffChangeValue,
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