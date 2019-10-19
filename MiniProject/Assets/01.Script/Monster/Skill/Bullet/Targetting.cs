using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Targetting : MonoBehaviour
{
    float timer;

    public Meteor bullet;
    private Meteor set;

    // Start is called before the first frame update
    private void Awake()
    {
        set = Instantiate(bullet,
            gameObject.transform.position + new Vector3(0, 0.7f, 0),
            Quaternion.Euler(0, 0, 0), 
            gameObject.transform);
        set.gameObject.SetActive(false);
    }

    public void Setting()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.0f)
        {
            set.gameObject.SetActive(true);
            if(timer > 2.0f)
            {
                set.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
