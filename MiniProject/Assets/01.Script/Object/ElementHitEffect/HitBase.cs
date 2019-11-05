using UnityEngine;


public class HitBase : MonoBehaviour
{
	public ParticleSystem particle;
    public void RemoveSet() { gameObject.SetActive(false); }

    private void Update()
    {
        if (!particle.isPlaying)
            gameObject.SetActive(false);
    }
}
