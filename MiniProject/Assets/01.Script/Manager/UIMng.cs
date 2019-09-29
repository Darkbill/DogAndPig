using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIMng : MonoBehaviour
{
	#region SINGLETON
	static UIMng _instance = null;

    public Inventory ItemInventory;

	public static UIMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UIMng)) as UIMng;
				if (_instance == null)
				{
					_instance = new GameObject("UIMng", typeof(UIMng)).GetComponent<UIMng>();
				}
			}

			return _instance;
		}
	}
	#endregion
}
