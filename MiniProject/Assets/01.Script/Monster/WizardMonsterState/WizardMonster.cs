public class WizardMonster : Monster
{
    public SkillWizardAttack attack;
	private void Start()
	{
		attack = Instantiate(attack);
	}
	public void ShotMeteo()
	{
		attack.StartingCount();
	}
	private void OnDestroy()
	{
		Destroy(attack);
	}
}
