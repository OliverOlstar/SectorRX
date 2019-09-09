using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    //Animation Handling for the player model - Oliver

    private PlayerStateController _stateController;
    private Animator _anim;

    [SerializeField] private float _rotationDampening = 11f;
    [SerializeField] private float _animSpeedDampening = 5f;

    void Start()
    {
        _stateController = GetComponentInParent<PlayerStateController>();
        _anim = GetComponentInChildren<Animator>();

        //Temp (animation's origins are screwed (my b))
        transform.GetChild(0).localPosition = new Vector3(0, -1.05f, 0);
    }
    
    void Update()
    {
        if (_stateController._movementComponent.disableMovement == false)
        {
            if (_stateController._movementComponent.moveDirection != Vector3.zero)
            {
                transform.forward = Vector3.Lerp(transform.forward, _stateController._movementComponent.moveDirection, Time.deltaTime * _rotationDampening);
            }

            _anim.SetFloat("Speed", Mathf.Lerp(_anim.GetFloat("Speed"), _stateController._movementComponent.moveDirection.magnitude, Time.deltaTime * _animSpeedDampening));
        }

        _anim.SetBool("OnGround", _stateController.OnGround);
    }

    public void StartDodge(Vector3 pFacing)
    {
        transform.forward = pFacing;
        _anim.SetBool("Dodge", true);
        _anim.SetFloat("Speed", 0);
    }

    public void StopDodge()
    {
        _anim.SetBool("Dodge", false);
    }

    public void StartJump(Vector3 pFacing)
    {
        transform.forward = pFacing;
        _anim.SetTrigger("Jump");
        _anim.SetFloat("Speed", 0);
    }

    public void SetOnGround(bool pOnGround)
    {
        _anim.SetBool("OnGround", pOnGround);
    }
}
