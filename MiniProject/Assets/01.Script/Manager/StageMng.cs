using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMng : MonoBehaviour
{
	#region SINGLETON
	static StageMng _instance = null;

	public static StageMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(StageMng)) as StageMng;
				if (_instance == null)
				{
					_instance = new GameObject("StageMng", typeof(StageMng)).GetComponent<StageMng>();
				}
			}

			return _instance;
		}
	}
	#endregion

}
