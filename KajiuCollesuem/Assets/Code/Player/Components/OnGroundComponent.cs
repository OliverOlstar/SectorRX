using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundComponent : MonoBehaviour
{
    [SerializeField] private float _isGroundedCheckDistance = 1.0f;

    [Space]
    [SerializeField] private float _inputInfluenceGrounded = 1.0f;
    [SerializeField] private float _inputInfluenceInAir = 0.7f;

    [Space]
    [SerializeField] private float _downForceRate = 6f;
    [SerializeField] private float _downForceTerminal = 4f;
    private float _downForce = 0;

    private PlayerStateController _stateController;
    private Rigidbody _rb;

    private void Awake()
    {
        _stateController = GetComponent<PlayerStateController>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Falling Force (Add extra force to falling to make falling feel better)
        FallingForce();

        //Check if on the ground
        CheckGrounded();
    }
    
    private void CheckGrounded()
    {
        //Raycast to check for if grounded
        RaycastHit hit;
        // On ground
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _isGroundedCheckDistance))
        {
            // Landed
            if (_stateController.onGround == false)
            {
                _stateController.onGround = true;
                
                // Anim
                _stateController._modelController.AddCrouching(_downForce / _downForceTerminal, 0.08f, 0.25f);

                // Shake
                _stateController._CameraShake.PlayShake(_downForce / _downForceTerminal * 2.0f, 5.0f, 0.1f, 0.3f);

                // Sound
                _stateController._Sound.LandingSound(0.0f);
            }
        }
        // Off ground
        else if (_stateController.onGround == true)
        {
            _stateController.onGround = false;
        }
    }

    public void ResetFallingForce()
    {
        _downForce = 0;
    }

    public void FallingForce()
    {
        //Change the amount of influence player input has on the player movement based on wether he is grounded or not
        if (_stateController.onGround)
        {
            _stateController._movementComponent.inputInfluence = _inputInfluenceGrounded;
            _downForce = 0;
        }
        else
        {
            _stateController._movementComponent.inputInfluence = _inputInfluenceInAir;

            //Add force downwards which adds ontop of gravity
            if (_downForce < _downForceTerminal)
                _downForce += _downForceRate * Time.deltaTime;
            else
                _downForce = _downForceTerminal;
        }

        _rb.AddForce(Vector3.down * _downForce * Time.deltaTime);
    }
}
