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
