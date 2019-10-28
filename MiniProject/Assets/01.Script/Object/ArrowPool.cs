using UnityEngine;
using System.Collections.Generic;
public class ArrowPool : MonoBehaviour
{
	private List<Arrow> arrowList = new List<Arrow>();
	public Arrow arrow;
	private const int PoolSiz = 10;
	private void Awake()
	{
		CreateArrow();
	}
	private void CreateArrow()
	{
		for (int i = 0; i < PoolSiz; ++i)
		{
			GameObject o = Instantiate(arrow.gameObject);
			o.transform.position = gameObject.transform.position;
			o.transform.parent = gameObject.transform;
			o.SetActive(false);
			arrowList.Add(o.GetComponent<Arrow>());
		}
	}
	public void SetArrow(Vector3 arrowPos,Vector3 dir,float damage)
	{
		for(int i = 0; i < arrowList.Count; ++i)
		{
			if(arrowList[i].gameObject.activeSelf == false)
			{
				arrowList[i].Setting(arrowPos, dir, damage);
				return;
			}
		}
		CreateArrow();
		SetArrow(arrowPos,dir,damage);
	}
}
