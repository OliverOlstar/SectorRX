using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDebugger : MonoBehaviour
{
    public PlayerStateController controller;

    public float vertical;
    public float horizontal;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = controller.verticalInput;
        horizontal = controller.horizontalInput;
    }
}
