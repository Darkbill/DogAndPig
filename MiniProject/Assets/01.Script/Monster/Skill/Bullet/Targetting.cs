using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Targetting : MonoBehaviour
{
    public delegate void RunningMeteor(Vector3 pos);
    public static event RunningMeteor MeteorRun;


    public void EndEvent()
    {
        gameObject.SetActive(false);
        MeteorRun(gameObject.transform.position + new Vector3(0, 0.7f, 0));
    }
}
