using UnityEngine;
using UnityEngine.UI;
public class HPUI : MonoBehaviour
{
	private Monster monster;
	public Image hpImage;
	public Image hpBG;
	public Text hpText;
	public void Setting(Monster _monster)
	{
		monster = _monster;
		gameObject.SetActive(true);
	}
	public void SetOff()
	{
		monster = null;
		gameObject.SetActive(false);
	}
	private void Update()
	{
		float cHp = monster.monsterData.healthPoint;
		if (cHp <= 0)
		{
			SetOff();
			return;
		}
		gameObject.transform.position = monster.gameObject.transform.position + Vector3.up;
		hpText.text = cHp.ToString();
		hpImage.fillAmount = cHp / JsonMng.Ins.monsterDataTable[monster.MonsterID].healthPoint;
	}
}
