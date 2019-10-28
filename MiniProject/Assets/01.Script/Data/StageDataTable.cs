public class StageDataTable : TableBase
{
	public int stageID;
	public int stageLevel;
	public int enemyIndex;
	public int enemyLevel;
	public float enemyPosX;
	public float enemyPosY;
	public int boss;

	public override float GetTableID()
	{
		return stageID;
	}
	public StageDataTable()
	{

	}
	public StageDataTable(int s,int sL,int eI,int eL,float eX,float eY,int b)
	{
		stageID = s;
		stageLevel = sL;
		enemyIndex = eI;
		enemyLevel = eL;
		enemyPosX = eX;
		enemyPosY = eY;
		boss = b;
	}
}
