using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class ItemInfoUI : MonoBehaviour
{
	public Image itemImage;
	public Image frameImage;
	public Text itemNameText;
	public Text itemBaseInfoText;
	public Text skillInfoText;
	public void ShowItemlInfo(ItemData item)
	{
		itemImage.sprite = SpriteMng.Ins.itemAtlas.GetSprite(item.itemName);
		frameImage.sprite = SpriteMng.Ins.frameAtlas.GetSprite(item.itemGradeType.ToString());
		itemNameText.text = item.itemName;
		itemBaseInfoText.text = Define.GetItemBaseInfoText(item);
		skillInfoText.text =    Define.GetItemSkillInfoText(item);
		gameObject.SetActive(true);

	}
}
