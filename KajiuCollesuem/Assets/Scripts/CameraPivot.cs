using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public GameObject Target;
    public Vector3 offSet = new Vector3(0, 1, 0);
    
    void Update()
    {
        //Position the camera pivot on the player
        transform.position = Target.transform.position + offSet;
    }
}
