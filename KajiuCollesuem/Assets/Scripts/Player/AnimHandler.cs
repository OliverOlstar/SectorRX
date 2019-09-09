using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    //Animation Handling for the player model - Oliver

    private Animator _anim;

    private PlayerStateController _stateController;
    [SerializeField] private float _rotationDampening = 5f;
    [SerializeField] private float _animSpeedDampening = 5f;

    void Start()
    {
        _stateController = GetComponentInParent<PlayerStateController>();
        _anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        if (_stateController._movementComponent.moveDirection != Vector3.zero && _stateController._movementComponent.disableMovement == false)
        {
            transform.forward = Vector3.Lerp(transform.forward, _stateController._movementComponent.moveDirection, Time.deltaTime * _rotationDampening);
        }

        _anim.SetFloat("Speed", Mathf.Lerp(_anim.GetFloat("Speed"), _stateController._movementComponent.moveDirection.magnitude, Time.deltaTime * _animSpeedDampening));
    }

    public void StartDodge(Vector3 pFacing)
    {
        transform.forward = pFacing;
        _anim.SetBool("Dodge", true);
    }

    public void StopDodge()
    {
        _anim.SetBool("Dodge", false);
    }
}
