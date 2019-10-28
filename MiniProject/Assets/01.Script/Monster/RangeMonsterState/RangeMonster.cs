using UnityEngine;
public class RangeMonster : Monster
{
	public GameObject arrowImage;
	private Vector3 arrowDir;
	public override bool AttackCheckStart()
	{
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (att.BaseAttack(gameObject.transform.right, directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
		{
			arrowDir = GameMng.Ins.player.transform.position - gameObject.transform.position + new Vector3(0,0.3f,0);
			arrowDir.z = 0;
			arrowDir.Normalize();
			return true;
		}
		return false;
	}
	public void ShotArrow()
	{
		GameMng.Ins.objectPool.arrowPool.SetArrow(arrowImage.transform.position, arrowDir, monsterData.damage);
	}
}