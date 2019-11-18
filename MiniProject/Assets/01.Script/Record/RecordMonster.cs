public class RecordMonster : Monster
{
	private void Start()
	{
		Angle = -180;
		ChangeAnimation(GlobalDefine.eMonsterAnimation.Idle);
	}
}
