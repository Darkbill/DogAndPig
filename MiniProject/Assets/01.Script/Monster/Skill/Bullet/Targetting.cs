using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Targetting : MonoBehaviour
{
	public static Dictionary<int, Action<Vector3>> actionDict = new Dictionary<int, Action<Vector3>>();
	public int skillID;

    public ParticleSystem effectSystem;

    public void Playing()
    {
        effectSystem.Play();
    }
    public void EndPlaying()
    {
        effectSystem.Stop();
    }

    private void Update()
    {
        if (!effectSystem.isPlaying)
        {
            gameObject.SetActive(false);
            actionDict[skillID](gameObject.transform.position);
        }
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
