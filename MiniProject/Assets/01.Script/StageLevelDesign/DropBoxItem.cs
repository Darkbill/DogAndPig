using UnityEngine;
using UnityEngine.UI;
public class DropBoxItem : MonoBehaviour
{
	public Text stageText;
	public Toggle toggle;
	private void Update()
	{
		if(toggle.isOn == true)
		{
			LevelDesignMng.Ins.ShowStageInfo(int.Parse(stageText.text));
		}
	}
}
