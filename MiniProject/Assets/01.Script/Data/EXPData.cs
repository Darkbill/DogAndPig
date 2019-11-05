public class EXPData : TableBase
{
	public int level;
	public int requiredExp;
	public int cumulativeExp;
	public override int GetTableID()
	{
		return level;
	}
}
