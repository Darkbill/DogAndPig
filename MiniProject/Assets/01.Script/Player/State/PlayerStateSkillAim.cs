using UnityEngine;
using GlobalDefine;
public class PlayerStateSkillAim : PlayerState
{
	public float fHorizontal;
	public float fVertical;
	public Movement Mov = new Movement();

	public float rightHorizontal;
	public float rightVertical;

	public PlayerStateSkillAim(Player o) : base(o)
	{
	}

	public override void OnStart()
	{
		
	}
	public override bool OnTransition()
	{
		if(Input.GetMouseButtonUp(0))
		{
			GameMng.Ins.EndSkillAim();
			return true;
		}
		return false;
	}

	public override void Tick()
	{
		if (OnTransition()) return;
		Moving();
	}
	public override void OnEnd()
	{

	}


	private void Moving()
	{
		fHorizontal = Input.GetAxis("Horizontal");
		fVertical = Input.GetAxis("Vertical");
#if UNITY_EDITOR_WIN
		Mov.iSpeed = playerObject.calStat.moveSpeed;
		playerObject.transform.position += Mov.Move(fHorizontal, fVertical);
		//TODO : 모바일 부분에 해당 조이스틱의 움직임 값을 가져와 세팅
#else
		Vector3 direction = UIMngInGame.Ins.GetJoyStickDirection();
        playerObject.transform.position += new Vector3(direction.x, direction.y, 0) * playerObject.calStat.moveSpeed * Time.deltaTime;
#endif
		Vector3 aimPos = Camera.main.ScreenToWorldPoint(UIMngInGame.Ins.aimImage.transform.position);
		Vector3 dir = aimPos - playerObject.transform.position;
		playerObject.degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
	}
}
