using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTeleport : MonoBehaviour
{
    public ConfirmationArea target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
            gameObject.transform.position = TeleportPos(gameObject.transform.position);
    }

    public Vector3 TeleportPos(Vector3 pos)
    {
        return target.TargetSetting(pos);
    }
}
