using GlobalDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitBase : MonoBehaviour
{ 
    public void RemoveSet() { gameObject.SetActive(false); }

    private void Update()
    {
        if (!gameObject.GetComponent<ParticleSystem>().isPlaying)
            gameObject.SetActive(false);
    }
}
