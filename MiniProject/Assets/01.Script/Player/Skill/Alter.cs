using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alter : MonoBehaviour
{
    public float Speed;
    float Damage = 1;

    public Vector3 TargetPos = new Vector3();

    public bool StartMove;
    public void Setting(Vector3 startPos)
    {
        gameObject.SetActive(true);
        StartMove = true;
        gameObject.transform.position = startPos;
    }
    void Update()
    {
        if(StartMove)
        {
            Vector3 movement = TargetPos - gameObject.transform.position;
            movement.z = 0;
            if(Vector3.Distance(transform.position, TargetPos) < 0.2f ||
                Vector3.Distance(transform.position, GameMng.Ins.player.transform.position) < 0.2f)
            {
                gameObject.SetActive(false);
                StartMove = false;
            }
            else
            {
                gameObject.transform.position += movement * Time.deltaTime * Speed;
            }
        }
    }
}
