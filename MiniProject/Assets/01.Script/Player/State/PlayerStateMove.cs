using UnityEngine;
using GlobalDefine;
public class PlayerStateMove : PlayerState
{
	public float fHorizontal;
	public float fVertical;
	public Movement Mov = new Movement();

    public float rightHorizontal;
    public float rightVertical;

	public PlayerStateMove(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
		playerObject.ChangeAnimation(ePlayerAnimation.Run);
    }

	public override bool OnTransition()
	{

#if UNITY_EDITOR_WIN

        float movesiz = Time.deltaTime;

        fHorizontal = Input.GetAxis("Horizontal");
        fVertical = Input.GetAxis("Vertical");

        rightHorizontal = Input.GetAxis("HorizontalArrow");
        rightVertical = Input.GetAxis("VerticalArrow");
        Debug.DrawRay(playerObject.transform.position, new Vector3(rightHorizontal, rightVertical));
#else
		if (playerObject.isMove == false)
			return true;
        return false;
#endif
        if (fVertical == 0 && fHorizontal == 0)
        {
            return true;
        }
        return false;
	}

	public override void Tick()
	{
		if (OnTransition() == true)
		{
			playerObject.playerStateMachine.ChangeState(ePlayerState.Idle);
			return;
		}
		Moving();
		//Attack();
	}
	public override void OnEnd()
	{

	}
    private void Attack()
	{
		if (playerObject.playerStateMachine.AttackDelay())
		{
			GameMng.Ins.player.AttackStart();
		}
	}
	

	private void Moving()
	{
#if UNITY_EDITOR_WIN
        Mov.iSpeed = playerObject.calStat.moveSpeed;
        playerObject.transform.position += Mov.Move(fHorizontal, fVertical);
        if(rightHorizontal != 0 || rightVertical != 0)
            playerObject.degree = Mathf.Atan2(rightVertical, rightHorizontal) * Mathf.Rad2Deg;
        else
            playerObject.degree = Mathf.Atan2(fVertical, fHorizontal) * Mathf.Rad2Deg;

#else
		Vector3 direction = UIMngInGame.Ins.GetJoyStickDirection();
        playerObject.transform.position += new Vector3(direction.x, direction.y, 0) * playerObject.calStat.moveSpeed * Time.deltaTime;
        playerObject.degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
#endif
        if (playerObject.isAim)
		{
			Vector3 aimPos = Camera.main.ScreenToWorldPoint(UIMngInGame.Ins.aimImage.transform.position);
			Vector3 dir = aimPos - playerObject.transform.position;
			playerObject.degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if (Input.GetMouseButtonUp(0))
			{
				GameMng.Ins.EndSkillAim();
			}
		}
    }
}
