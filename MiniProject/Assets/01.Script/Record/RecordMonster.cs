public class RecordMonster : Monster
{
	private void Awake()
	{
		Angle = -180;
		ChangeAnimation(GlobalDefine.eMonsterAnimation.Idle);
		MonsterSetting();
	}
}
