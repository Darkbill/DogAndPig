using UnityEngine;

public class SkillWizardAttack : MonoBehaviour
{
    public AttackMeteor Attack;
    public bool Target = false;

    private float countTime = 0.0f;
    private const float maxCountTime = 1.0f;
    
    void Update()
    {
        if (Target)
        {
            countTime += Time.deltaTime;
			if (countTime >= maxCountTime)
			{
				Target = false;
				countTime = 0.0f;
				Attack.gameObject.SetActive(true);
			}
        }
        else if(!Target && !Attack.gameObject.activeSelf)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            countTime += Time.deltaTime;
            if(countTime >= maxCountTime / 2)
            {
                countTime = 0.0f;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void StartingCount()
    {
        if (!Target)
        {
            gameObject.transform.position = GameMng.Ins.player.transform.position;
            Target = true;
            Attack.Setting();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damage(GlobalDefine.eAttackType.Fire, 10);
        }
    }
}
