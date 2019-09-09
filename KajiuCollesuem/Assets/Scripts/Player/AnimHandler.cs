using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    private PlayerStateController stateController;
    [SerializeField] private float rotationDampening = 5f;

    void Start()
    {
        stateController = GetComponentInParent<PlayerStateController>();
    }
    
    void Update()
    {
        if (stateController._movementComponent.moveDirection != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, stateController._movementComponent.moveDirection, Time.deltaTime * rotationDampening);
        }
        Debug.Log(stateController._movementComponent.moveDirection);
    }
}
