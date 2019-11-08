using UnityEngine;
using UnityEngine.UI;
public class DropBoxWorldItem : MonoBehaviour
{
	public Text worldText;
	public Toggle toggle;
	private void Update()
	{
		if (toggle.isOn == true)
		{
			LevelDesignMng.Ins.ShowStageInfoToWorld(int.Parse(worldText.text));
		}
	}
}
