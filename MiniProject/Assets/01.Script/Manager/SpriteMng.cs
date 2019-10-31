using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class SpriteMng : MonoBehaviour
{
	#region SINGLETON
	static SpriteMng _instance = null;

	public static SpriteMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(SpriteMng)) as SpriteMng;
				if (_instance == null)
				{
					_instance = new GameObject("SpriteMng", typeof(SpriteMng)).GetComponent<SpriteMng>();
				}
			}

			return _instance;
		}
	}
	#endregion
	private void Awake()
	{
		DontDestroyOnLoad(this);
		itemAtlas = Resources.Load(string.Format("Atlas/{0}","ItemAtlas"), typeof(SpriteAtlas)) as SpriteAtlas;
		skillAtlas = Resources.Load(string.Format("Atlas/{0}", "SkillAtlas"), typeof(SpriteAtlas)) as SpriteAtlas;
	}
	public SpriteAtlas itemAtlas;
	public SpriteAtlas skillAtlas;
}
