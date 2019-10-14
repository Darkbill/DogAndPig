using UnityEngine;
using UnityEngine.UI;
public class BuffSlot : MonoBehaviour
{
	public Image buffImage;
	public Text timerText;
	int skillIndex;
	public void SetBuff(int sI)
	{
		gameObject.SetActive(true);
		buffImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillIndex));
		skillIndex = sI;
	}
	private void Update()
	{
		int t = GameMng.Ins.player.GetTime(skillIndex);
		if (t == 0) gameObject.SetActive(false);
		timerText.text = t.ToString();
		
	}
}
