using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    //Animation Handling for the player model - Oliver

    private PlayerStateController _stateController;
    [SerializeField] private float _rotationDampening = 5f;

    void Start()
    {
        _stateController = GetComponentInParent<PlayerStateController>();
    }
    
    void Update()
    {
        if (_stateController._movementComponent.moveDirection != Vector3.zero && _stateController._movementComponent.disableMovement == false)
        {
            transform.forward = Vector3.Lerp(transform.forward, _stateController._movementComponent.moveDirection, Time.deltaTime * _rotationDampening);
        }
    }
}
