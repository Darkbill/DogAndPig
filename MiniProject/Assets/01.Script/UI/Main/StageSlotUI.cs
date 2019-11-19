using UnityEngine;
using UnityEngine.UI;
using GlobalDefine;
public class StageSlotUI : MonoBehaviour
{

	public int worldID;
	public Image stageIconImage;
	public Image challengeIconImage;
	public Text worldLevelText;
	
	public void Setting(eStageType type)
	{
		switch(type)
		{
			case eStageType.Challenge:
				challengeIconImage.gameObject.SetActive(true);
				challengeIconImage.sprite = SpriteMng.Ins.stageAtlas.GetSprite(string.Format("ChallengeIcon"));
				stageIconImage.sprite = SpriteMng.Ins.stageAtlas.GetSprite(string.Format("Challenge"));
				break;
			case eStageType.Clear:
				challengeIconImage.gameObject.SetActive(true);
				challengeIconImage.sprite = SpriteMng.Ins.stageAtlas.GetSprite(string.Format("ClearIcon"));
				stageIconImage.sprite = SpriteMng.Ins.stageAtlas.GetSprite(string.Format("Clear"));
				break;
			case eStageType.Lock:
				stageIconImage.sprite = SpriteMng.Ins.stageAtlas.GetSprite(string.Format("Lock"));
				GetComponent<Button>().enabled = false;
				break;
		}
	}
	public void OnClickStage()
	{
		GameMng.worldLevel = worldID;
		GameMng.stageLevel = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}

    public void OnClickStageTest()
    {
        GameMng.worldLevel = worldID;
        GameMng.stageLevel = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("RunningTest");
    }
}
