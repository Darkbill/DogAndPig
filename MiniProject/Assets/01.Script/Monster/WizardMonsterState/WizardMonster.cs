public class WizardMonster : Monster
{
    public SkillWizardAttack attack;
	public void ShotMeteo()
	{
		SkillWizardAttack o = Instantiate(attack);
	}
}
