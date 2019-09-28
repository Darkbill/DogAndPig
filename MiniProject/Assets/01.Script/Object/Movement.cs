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
}
