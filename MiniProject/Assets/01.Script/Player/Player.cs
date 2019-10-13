using GlobalDefine;
using UnityEngine;
public class Player : MonoBehaviour
{
	// * public * //
	public bool isMove;
	public PlayerData calStat;
	public PlayerData skillStat = new PlayerData();
	public PlayerStateMachine playerStateMachine;
	public SpriteRenderer playerSprite;
	// * private * //
	private int level = 1;

	private ConditionData[] conditionArr = new ConditionData[(int)eBuffType.Max];

	/* 테스트 코드 */
	public int[] skillArr = new int[3];
    private void Awake()
	{
		skillArr[0] = 1;
		skillArr[1] = 2;
		skillArr[2] = 3;
		for(int i = 0; i < conditionArr.Length; ++i)
		{
			conditionArr[i] = new ConditionData();
		}
		PlayerSetting();
	}
	private void PlayerSetting()
	{
		CalculatorStat();
	}
	private void Update()
	{
		UpdateBuff(Time.deltaTime);
		//테스트코드
		AddBuff(new ConditionData(eBuffType.PhysicsStrong, 100, 100000));
		if (isMove)
		{
			MoveToJoyStick();
		}
	}
	private void UpdateBuff(float delayTime)
	{
		for(int i = 0; i < conditionArr.Length; ++i)
		{
			if(conditionArr[i].activeFlag)
			{
				conditionArr[i].currentTime -= delayTime;
				if(conditionArr[i].currentTime <= 0)
				{
					conditionArr[i].activeFlag = false;
					CalculatorStat();
				}
			}
		}
	}
    public void MoveToJoyStick()
	{
		Vector3 direction = UIMngInGame.Ins.GetJoyStickDirection();
		gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * calStat.moveSpeed * Time.deltaTime;

        float Degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, Degree);
	}
	public void Damage(eAttackType attackType, float damage)
	{
		float d = (damage - calStat.armor) * calStat.GetResist(attackType).CalculatorDamage();
		DamageResult((int)d);
	}
	public void Damage(eAttackType attackType, float damage, ConditionData condition)
	{
		float d = (damage - calStat.armor) * calStat.GetResist(attackType).CalculatorDamage();
		bool isBuff = calStat.GetResist(attackType).GetBuff();
		if(isBuff)
		{
			conditionArr[(int)condition.buffType].SetBuff(condition.SustainmentTime, condition.changeValue);
			CalculatorStat();
		}
		DamageResult((int)d);
	}
	public void DamageResult(int d)
	{
		if (d < 1) d = 1;
		calStat.healthPoint -= d;
		UIMngInGame.Ins.DamageToPlayer(d);
		if (calStat.healthPoint <= 0) GameMng.Ins.GameOver();
	}
	public void AddBuff(ConditionData condition)
	{
		conditionArr[(int)condition.buffType].SetBuff(condition.SustainmentTime, condition.changeValue);
		CalculatorStat();
	}
	private void CalculatorStat()
	{
		//TODO : 레벨에 의한 스탯계산
		calStat = JsonMng.Ins.playerDataTable[1].AddStat(skillStat,conditionArr);
	}

	public float GetFullHP()
	{
		return skillStat.healthPoint + JsonMng.Ins.playerDataTable[level].healthPoint;
	}
	private void LevelUP()
	{
		CalculatorStat();
	}
}
