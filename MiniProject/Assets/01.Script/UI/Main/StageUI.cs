using System.Collections.Generic;
using UnityEngine;
using GlobalDefine;
public class StageUI : MonoBehaviour
{
	public List<StageSlotUI> stageSlotUIList = new List<StageSlotUI>();
	private void OnEnable()
	{
		Setting();
	}
	private void Setting()
	{
		for(int i = 0; i < stageSlotUIList.Count; ++i)
		{
			if(stageSlotUIList[i].worldID <= JsonMng.Ins.playerInfoDataTable.clearLevel) stageSlotUIList[i].Setting(eStageType.Clear);
			else if (stageSlotUIList[i].worldID == JsonMng.Ins.playerInfoDataTable.clearLevel+1) stageSlotUIList[i].Setting(eStageType.Challenge);
			else stageSlotUIList[i].Setting(eStageType.Lock);
		}
	}
}
