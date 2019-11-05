public class StageDataTable : TableBase
{
	public int stageLevel;
	public int enemyIndex;
	public int enemyLevel;
	public float enemyPosX;
	public float enemyPosY;
	public int boss;

	public override int GetTableID()
	{
		return stageLevel;
	}
	public StageDataTable()
	{

	}
	public StageDataTable(int sL,int eI,int eL,float eX,float eY,int b)
	{
		stageLevel = sL;
		enemyIndex = eI;
		enemyLevel = eL;
		enemyPosX = eX;
		enemyPosY = eY;
		boss = b;
	}
}
