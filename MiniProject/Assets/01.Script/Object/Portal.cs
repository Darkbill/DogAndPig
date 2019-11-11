using UnityEngine;
using DG.Tweening;


public class Portal : MonoBehaviour
{
	public CircleCollider2D col;
    public ParticleSystem portalCreateEffectSystem;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		UIMngInGame.Ins.Fade(true);
		col.enabled = false;
		gameObject.transform.localScale = Vector3.zero;
		gameObject.SetActive(false);
	}

    public void NextStagePotal()
    {
        portalCreateEffectSystem.Play();
		gameObject.SetActive(true);
		gameObject.transform.DOScale(1.0f, 0.7f).OnComplete(() => { col.enabled = true; });
    }
}
