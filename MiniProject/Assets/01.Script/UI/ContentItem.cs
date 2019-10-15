using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
public class ContentItem : MonoBehaviour
{
	public Image skillImage;
	public Text skillText;
	public Text priceText;
	public void Setting(float skillID)
	{
		gameObject.SetActive(true);
		skillImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillID));
		skillText.text = "";
		priceText.gameObject.transform.parent.name = "";
	}
}