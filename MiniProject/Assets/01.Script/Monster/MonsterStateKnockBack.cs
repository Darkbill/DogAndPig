using UnityEngine;
using GlobalDefine;
public class MonsterStateKnockBack : MonsterState
{
	private float setspeed;
	private Vector3 range;

	public MonsterStateKnockBack(Monster o) : base(o)
	{
	}

	public override void OnStart()
	{
		monsterObject.ChangeAnimation(eMonsterAnimation.Idle);
		setspeed = Define.knockBackSpeed * monsterObject.monsterStateMachine.knockBackPower;
		range = monsterObject.monsterStateMachine.knockBackDir;
		range.Normalize();
	}

	public override bool OnTransition()
	{
        //TODO : right, left, up, down vec
        Ray2D[] rayarray = new Ray2D[4];
        rayarray[0] = new Ray2D(monsterObject.transform.position + new Vector3(0, monsterObject.monsterData.size),
            Vector3.right);
        rayarray[1] = new Ray2D(monsterObject.transform.position + new Vector3(0, monsterObject.monsterData.size),
            -Vector3.right);
        rayarray[2] = new Ray2D(monsterObject.transform.position + new Vector3(0, monsterObject.monsterData.size),
            Vector3.up);
        rayarray[3] = new Ray2D(monsterObject.transform.position + new Vector3(0, monsterObject.monsterData.size),
            -Vector3.up);

        foreach(Ray2D ray in rayarray)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.5f, 1 << LayerMask.NameToLayer("Wall"));
            if (hit.collider == null) break;
            if (hit.collider.CompareTag("Wall"))
                return true;
        }

        if (setspeed <= 0)
		{
			monsterObject.monsterStateMachine.ChangeStateIdle();
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true) return;
		KnockBack();
	}

	private void KnockBack()
	{
		monsterObject.transform.position += range * Time.deltaTime * setspeed;
		setspeed -= Time.deltaTime * 100;
	}

	public override void OnEnd()
	{
		setspeed = 0;
	}
}
