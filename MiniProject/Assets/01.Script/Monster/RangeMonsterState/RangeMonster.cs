using UnityEngine;
public class RangeMonster : Monster
{
	public GameObject arrowImage;
	public Arrow arrow;
	private Vector3 arrowDir;
	public override bool AttackCheckStart()
	{
		Vector3 directionToPlayer = GameMng.Ins.player.transform.position - gameObject.transform.position;
		if (att.BaseAttack(gameObject.transform.right, directionToPlayer, monsterData.attackRange, monsterData.attackAngle))
		{
			arrowDir = GameMng.Ins.player.transform.position - gameObject.transform.position;
			arrowDir.z = 0;
			arrowDir.Normalize();
			return true;
		}
		return false;
	}
	public void ShotArrow()
	{
		//TODO : Arrow 풀링
		GameObject o = Instantiate(arrow.gameObject,transform);
		o.GetComponent<Arrow>().Setting(arrowImage.transform.position, arrowDir,monsterData.damage);
	}
}