using UnityEngine;
public class ShopUI : MonoBehaviour
{
	public InfinityScroll infinityScoll;
	public SkillSlotUI[] skillSlotArr; 
	public void Setting()
	{
		infinityScoll.Setting();
		PlayerInfoData p = JsonMng.Ins.playerInfoDataTable;
		for(int i = 0; i < skillSlotArr.Length; ++i)
		{
			skillSlotArr[i].Setting(p.setSkillList[i]);
		}
	}
	public void ReNew()
	{
		PlayerInfoData p = JsonMng.Ins.playerInfoDataTable;
		for (int i = 0; i < skillSlotArr.Length; ++i)
		{
			skillSlotArr[i].Setting(p.setSkillList[i]);
		}
		ChangeSelectFlag(false);
	}
	public void ChangeSelectFlag(bool flag)
	{
		for (int i = 0; i < skillSlotArr.Length; ++i)
		{
			skillSlotArr[i].changeFlag = flag;
		}
	}
}
