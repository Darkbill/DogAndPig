using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour
{
	public Text frameText;
	public float worstFrame;
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
		string frameStr = string.Format("{0:F2} fps", frame);
		frameText.text = string.Format("{0} {1}", frameTimeStr, frameStr);
	}
}
