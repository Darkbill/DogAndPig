using UnityEngine;
using System.Collections.Generic;
public class HPUIMng : MonoBehaviour
{
	public List<HPUI> hpUIList = new List<HPUI>();
	public void Setting(Monster monster)
	{
		GetActiveUI().Setting(monster);
	}
	public HPUI GetActiveUI()
	{
		for(int i = 0; i < hpUIList.Count; ++i)
		{
			if(hpUIList[i].gameObject.activeSelf == false)
			{
				return hpUIList[i];
			}
		}
		GameObject o = Instantiate(hpUIList[0].gameObject, gameObject.transform);
		hpUIList.Add(o.GetComponent<HPUI>());
		return hpUIList[hpUIList.Count - 1];
	}
}
