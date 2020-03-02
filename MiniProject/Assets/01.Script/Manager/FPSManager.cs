using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour
{
	public Text frameText;
	private float worstFrame = float.MaxValue;
	public float averagerFrame;
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	private void Update()
	{
		float frameTime = Time.deltaTime;
		string frameTimeStr = string.Format("{0:F2} ms", frameTime * 1000);
		float frame = 1 / frameTime;
		Debug.Log(frame);
		if(worstFrame > frame)
		{
			worstFrame = frame;
		}
		string frameStr = string.Format("{0:F0} fps", frame);
		frameText.text = string.Format("{0} {1} worst : {2:F0}", frameTimeStr, frameStr, worstFrame);
	}
}
