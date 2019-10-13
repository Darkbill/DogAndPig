using GlobalDefine;
public class ConditionData
{
	public eBuffType buffType;
	public float SustainmentTime;
	public float currentTime;
	public float changeValue;
	public bool activeFlag;
	public ConditionData()
	{
		SustainmentTime = 0;
		changeValue = 0;
	}
	public ConditionData(eBuffType bT, float s,float c)
	{
		buffType = bT;
		SustainmentTime = s;
		currentTime = s;
		changeValue = c;
		activeFlag = true;
	}
	public void Set(eBuffType bT, float s, float c)
	{
		buffType = bT;
		SustainmentTime = s;
		currentTime = s;
		changeValue = c;
	}
	public void SetBuff(float s,float c)
	{
		SustainmentTime = s;
		currentTime = s;
		changeValue = c;
		activeFlag = true;
	}
	public void SetOff()
	{
		activeFlag = false;
		changeValue = 0;
	}
}
