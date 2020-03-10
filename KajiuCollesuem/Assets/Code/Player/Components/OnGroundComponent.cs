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

    private float _stuckTimer = 0.0f;

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
                _stateController._CameraShake.PlayShake(_downForce / _downForceTerminal * 3.0f, 10.0f, 0.1f, 0.32f, 0.01f);

                // Sound
                _stateController._Sound.LandingSound();
            }

            if (hit.collider.tag == "Sand")
                _stateController.groundMaterial = 0;
            else if (hit.collider.tag == "Metal")
                _stateController.groundMaterial = 1;
            else
                _stateController.groundMaterial = -1;
        }
        // Off ground
        else if (_stateController.onGround == true)
        {
            _stateController.onGround = false;
            _stateController.groundMaterial = -1;
        }
    }

    public void ResetFallingForce()
    {
        _downForce = 0;
    }

    public void FallingForce()
    {
        // Change the amount of influence player input has on the player movement based on wether he is grounded or not
        if (_stateController.onGround)
        {
            _stateController._movementComponent.inputInfluence = _inputInfluenceGrounded;
            _downForce = 0;
            _stuckTimer = 0;
        }
        else
        {
            _stateController._movementComponent.inputInfluence = _inputInfluenceInAir;

            // Add force downwards which adds ontop of gravity
            if (_downForce < _downForceTerminal)
                _downForce += _downForceRate * Time.deltaTime;
            else
                _downForce = _downForceTerminal;

            // Falling but not moving = stuck
            if (_rb.velocity.magnitude <= 0.001f)
            {
                _stuckTimer += Time.deltaTime;

                if (_stuckTimer > 3.5f)
                {
                    _rb.AddForce(Vector3.up * 25, ForceMode.Impulse);
                    _stuckTimer = 0.0f;
                }
            }


        }

        _rb.AddForce(Vector3.down * _downForce * Time.deltaTime);
    }
}
