using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
	public BuffSlot[] buffSlotArr;
    //생성, 위치선정
	public void ActiveBuff(int skillIndex)
	{
		for (int i = 0; i < buffSlotArr.Length; ++i)
		{
			if (buffSlotArr[i].gameObject.activeSelf == false)
			{
				buffSlotArr[i].SetBuff(skillIndex);
				break;
			}
		}
	}
	public void Sort()
	{
		for (int i = 0; i < buffSlotArr.Length; ++i)
		{
			if (buffSlotArr[i].gameObject.activeSelf == false)
			{
				for (int j = i; j < buffSlotArr.Length; ++j)
				{
					if (buffSlotArr[j].gameObject.activeSelf == true)
					{
						buffSlotArr[i].SetBuff(buffSlotArr[j].skillIndex);
						buffSlotArr[j].gameObject.SetActive(false);
						break;
					}
				}
			}
		}
	}
}
