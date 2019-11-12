using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltIsSupport : MonoBehaviour
{
    private Vector3 target;
    private Vector3 rightvec;

    private const float maxRange = 1.5f;
    private float setTimer = 0.0f;

    public void Setting(int id, Vector3 _target, Vector3 _pos)
    {
        target = _target;
        rightvec = target - _pos;

        gameObject.transform.position = _pos;
        gameObject.transform.localScale = 
            new Vector3(rightvec.magnitude / maxRange, rightvec.magnitude / maxRange, 1);

        float angle = Mathf.Atan2(rightvec.y, rightvec.x) * Mathf.Rad2Deg;

        gameObject.transform.eulerAngles = new Vector3(0, 0, angle - 90);
        setTimer = 0.0f;
    }
    private void Update()
    {
        setTimer += Time.deltaTime;
        if (setTimer >= 0.5f)
            gameObject.SetActive(false);
    }

    public void AnimationEndEvent()
    {
        gameObject.SetActive(false);
    }
}
