using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Targetting : MonoBehaviour
{
	public static Dictionary<int, Action<Vector3>> actionDict = new Dictionary<int, Action<Vector3>>();
	public int skillID;
	public void EndEvent()
    {
        gameObject.SetActive(false);
		actionDict[skillID](gameObject.transform.position + new Vector3(0, 0.7f, 0));
    }
	public static void SetActionDict(int skillID,Action<Vector3> runSkill)
	{
		if(actionDict.ContainsKey(skillID))
		{
			actionDict.Remove(skillID);
		}
		actionDict.Add(skillID, runSkill);
	}
}
