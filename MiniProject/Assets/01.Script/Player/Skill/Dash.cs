using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SkillBase
{
	private GameObject[] afterimage = new GameObject[10];
	public override void ActiveSkill()
	{
		StartCoroutine(CreateAfterImage());
	}
	IEnumerator CreateAfterImage()
	{
		int afterImageCount = 0;
		while (afterImageCount != 10)
		{
			afterImageCount++;
			yield return new WaitForSeconds(0.1f);
			afterimage[afterImageCount].gameObject.transform.position = GameMng.Ins.player.gameObject.transform.position;
			afterimage[afterImageCount].GetComponent<SpriteRenderer>().sprite = GameMng.Ins.player.playerSprite.sprite;
		}
	}
}
