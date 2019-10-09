using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
	public PlayerSkillData playerSkillData;
	public abstract void ActiveSkill();
}
