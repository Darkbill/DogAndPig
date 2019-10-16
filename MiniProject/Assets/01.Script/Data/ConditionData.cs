using GlobalDefine;
public class ConditionData
{
	public eBuffType buffType;
	public int skillIndex;
	public float sustainmentTime;
	public float currentTime;
	public float changeValue;
	public ConditionData()
	{
		sustainmentTime = 0;
		changeValue = 0;
	}
	public ConditionData(eBuffType bT, int sI, float s,float c)
	{
		buffType = bT;
		skillIndex = sI;
		sustainmentTime = s;
		currentTime = s;
		changeValue = c;
        //changeValue의 값은 각 bufftype마다 정해진 숫자를 정의한다.
        //확률에 대한 값은 천분율 단위로 정의한다.
        //MoveFast,MoveSlow : 속도증가 및 감소에 대한 확률 값
		//PhysicsStrong, PhysicsWeek : 강화 및 약화에 대한 확률 값
        //NockBack, Stun : 넉백 및 스턴에 대한 확률 값
        //스턴의 경우 currenttime도 참조한다.
	}
	public void Set(ConditionData conditionData)
	{
		buffType = conditionData.buffType;
		skillIndex = conditionData.skillIndex;
		sustainmentTime = conditionData.sustainmentTime;
		currentTime = sustainmentTime;
		changeValue = conditionData.changeValue;
	}
}
