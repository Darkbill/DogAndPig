using UnityEngine;

public class AttackMeteor : MonoBehaviour
{
    Vector3 target = new Vector3(-0.3f, 0.45f);

    public void Setting()
    {
        gameObject.transform.localPosition = new Vector3(-6.31f, 6.7f, 0);
    }

    void Update()
    {
        if (gameObject.transform.localPosition.x <= target.x &&
            gameObject.transform.localPosition.y >= target.y)
        {
            gameObject.transform.localPosition +=
                new Vector3(Mathf.Cos(150), Mathf.Sin(150), 0) * Time.deltaTime * 20f;
            return;
        }
        else gameObject.SetActive(false);
    }
}
