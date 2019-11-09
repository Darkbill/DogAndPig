using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
	public CircleCollider2D col;
    public ParticleSystem portalCreateEffectSystem;
    public List<GameObject> mainEffect = new List<GameObject>();
	private void OnTriggerEnter2D(Collider2D collision)
	{
		UIMngInGame.Ins.Fade(true);
		gameObject.SetActive(false);
	}

    public void NextStagePotal()
    {
        portalCreateEffectSystem.Play();
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        foreach (GameObject o in mainEffect)
            o.transform.DOScale(1.0f, 0.7f);
        //StartCoroutine(StartSet());
    }
    //private IEnumerator StartSet()
    //{
    //    portalCreateEffectSystem.Play();
    //    //yield return new WaitForSeconds(0.5f);
    //    gameObject.GetComponent<CircleCollider2D>().enabled = true;
    //    foreach (GameObject o in mainEffect)
    //        o.transform.DOScale(1.0f, 0.7f);
    //    gameObject.transform.DOScale(1.0f, 0.5f);
    //}
}
