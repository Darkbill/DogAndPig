using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
	public BuffSlot[] buffSlotArr;
    //생성, 위치선정
	public void ActiveBuff(int skillIndex)
	{
		buffSlotArr[0].SetBuff(skillIndex);
	}
	public void Sort()
	{

	}
}
