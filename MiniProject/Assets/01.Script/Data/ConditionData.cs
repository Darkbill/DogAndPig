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
	public ConditionData(eBuffType bT, int _skillIndex, float _sustainmentTime, float _changeValue)
	{
		buffType = bT;
		skillIndex = _skillIndex;
		sustainmentTime = _sustainmentTime;
		currentTime = _sustainmentTime;
		changeValue = _changeValue;
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
