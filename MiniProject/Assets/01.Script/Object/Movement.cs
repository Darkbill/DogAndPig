using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int iSpeed;
    public Vector3 Move(float Hor, float Ver)
    {
        Vector3 vMovement = new Vector3(Hor, Ver, 0);
        return vMovement * iSpeed * Time.deltaTime;
    }

    public bool GetKeyBoardDown(string ch)
    {
        switch(ch)
        {
            case "w":
            case "a":
            case "s":
            case "d":
                if (Input.GetKeyDown(ch))
                    return true;
                return false;
        }
        return false;
    }
}
