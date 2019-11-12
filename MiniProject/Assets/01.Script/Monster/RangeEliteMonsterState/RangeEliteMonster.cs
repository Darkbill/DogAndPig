using UnityEngine;

public class RangeEliteMonster : Monster
{
	public GameObject arrowImage;
	private Vector3 arrowDir;
	public bool skillFlag;
	private const float updownScale = 0.25f;
	public override bool AttackCheck()
	{
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (att.BaseAttack(GetForward(), directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
		{
			arrowDir = GameMng.Ins.player.transform.position -
				gameObject.transform.position +
				new Vector3(0, GameMng.Ins.player.calStat.size, 0);
			arrowDir.z = 0;
			arrowDir.Normalize();
			return true;
		}
		return false;
	}
	public void ShotArrow()
	{
		if (skillFlag) ShotTripleShot();
		else GameMng.Ins.objectPool.arrowPool.SetArrow(arrowImage.transform.position, arrowDir, monsterData.damage);

	}
	public void ShotTripleShot()
	{
		skillFlag = false;
		GameMng.Ins.objectPool.arrowPool.SetArrow(arrowImage.transform.position, new Vector3(arrowDir.x + updownScale, arrowDir.y +  updownScale), monsterData.damage);
		GameMng.Ins.objectPool.arrowPool.SetArrow(arrowImage.transform.position, arrowDir, monsterData.damage);
		GameMng.Ins.objectPool.arrowPool.SetArrow(arrowImage.transform.position, new Vector3(arrowDir.x - updownScale, arrowDir.y - updownScale), monsterData.damage);
	}
}
