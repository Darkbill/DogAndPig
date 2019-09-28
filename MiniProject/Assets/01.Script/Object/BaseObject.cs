using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    protected int iHp;
    protected int iAtt;

    protected int iSpeed;

    public float fHorizontal { get; set; }
    public float fVertical { get; set; }

    public void SetObjectStartPos(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }


}
