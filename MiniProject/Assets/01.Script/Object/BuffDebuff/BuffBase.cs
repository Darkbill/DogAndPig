﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase : MonoBehaviour
{
    public GameObject settingObj;
    public bool inaction = false;

    // Start is called before the first frame update
    public void Setting(ConditionData condition, GameObject obj)
    {
        settingObj = obj;
        StartCoroutine(selecttime(condition));
    }

    // Update is called once per frame
    void Update()
    {
        if(settingObj != null)
            gameObject.transform.position = settingObj.transform.position;
        if (settingObj == null || 
            settingObj.GetComponent<Monster>().monsterData.healthPoint <= 0 ||
            settingObj.GetComponent<Monster>().ConditionMainGet() == null)
            gameObject.SetActive(false);
    }

    private IEnumerator selecttime(ConditionData condition)
    {
        yield return new WaitForSeconds(condition.sustainmentTime);
        if(condition.currentTime <= 0)
            gameObject.SetActive(false);
    }
}
