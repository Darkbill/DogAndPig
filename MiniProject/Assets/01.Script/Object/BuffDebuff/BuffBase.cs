using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase : MonoBehaviour
{
    public GameObject settingObj;

    // Start is called before the first frame update
    public void Setting(ConditionData condition, GameObject obj)
    {
        settingObj = obj;
        StartCoroutine(selecttime(condition.currentTime));
    }

    // Update is called once per frame
    void Update()
    {
        if(settingObj != null)
            gameObject.transform.position = settingObj.transform.position;
    }

    private IEnumerator selecttime(float time)
    {
        if(settingObj == null)
        {
            gameObject.SetActive(false);
            yield break;
        }

        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
