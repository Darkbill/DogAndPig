using UnityEngine;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour
{
	public InfinityScroll infinityScoll;
	public SkillSlotUI[] skillSlotArr; 
	public void Setting()
	{
		infinityScoll.Setting();
		gameObject.SetActive(false);
		PlayerInfoData p = JsonMng.Ins.playerInfoDataTable;
		for(int i = 0; i < skillSlotArr.Length; ++i)
		{
			skillSlotArr[i].Setting(p.setSkillList[i]);
		}
	}

	public void BuySkill()
	{

	}
	public void SetSkill()
	{

	}
}
