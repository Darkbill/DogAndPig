using UnityEngine;
using UnityEngine.UI;
public class BuffSlot : MonoBehaviour
{
	public Image buffImage;
	public Text timerText;
	public int skillIndex;
	public void SetBuff(int sI)
	{
		gameObject.SetActive(true);
		skillIndex = sI;
		buffImage.sprite = SpriteMng.Ins.skillAtlas.GetSprite(string.Format("Skill_{0}", skillIndex));
	}
	private void Update()
	{
		float t = GameMng.Ins.player.GetTime(skillIndex);
		//TODO : ㅋㅋ;;
		if (t <= 0.1f)
		{
			gameObject.SetActive(false);
			UIMngInGame.Ins.buffUI.Sort();
			return;
		}
		timerText.text = ((int)t).ToString();
		
	}
}
