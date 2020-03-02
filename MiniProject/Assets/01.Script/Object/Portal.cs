using UnityEngine;
using DG.Tweening;
using System.Collections;


public class Portal : MonoBehaviour
{
	public CircleCollider2D col;
    public ParticleSystem portalCreateEffectSystem;
    public bool goldclear = false;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		UIMngInGame.Ins.Fade(true);
		col.enabled = false;
		gameObject.transform.localScale = Vector3.zero;
		gameObject.SetActive(false);
        goldclear = true;
    }

    public void NextStagePotal()
    {
        gameObject.SetActive(true);
        portalCreateEffectSystem.Play();
        gameObject.transform.DOScale(1.0f, 0.7f).OnComplete(() => { col.enabled = true; });
    }
}
