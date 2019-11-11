using System.Collections;
using UnityEngine;

public abstract class BulletPlayerSkill : MonoBehaviour
{
	public abstract void Crash(Monster monster);
    public IEnumerator setSkill()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);
    }
}
