using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : MonoBehaviour
{
    public int Range;

    public float Degree = 0.0f;

    const int Count = 10;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Count; ++i)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckMovVec()
    {
        Degree = Vector3.Angle(GameMng.Ins.player.MoveVec, new Vector3());
    }
}
