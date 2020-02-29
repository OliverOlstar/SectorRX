using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerStateController _stateController;

    [HideInInspector] public float speedMult = 1;
    [HideInInspector] public float jumpMult = 1;

    [Header("Movement")]
    [SerializeField] private float _moveAcceleration = 1.0f;
    public float maxSpeed = 4.0f;

    [HideInInspector] public float inputInfluence = 1.0f;

    [Header("Jump")]
    [SerializeField] private float _jumpForceUp = 4;
    [SerializeField] private float _jumpForceVelocityMult = 1.7f;

    [Space]
    [SerializeField] private float _jumpGraceLength = 0.1f;
    private float _jumpGrace = 0;

    [Space]
    public bool disableMovement = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _stateController = GetComponent<PlayerStateController>();
    }

    void Update()
    {
        if (disableMovement) 
            return;

        if (_stateController.onGround)
            _jumpGrace = Time.time + _jumpGraceLength;

        //Movement
        Move();
    }

    public void OnJump()
    {
        if (_jumpGrace > Time.time && disableMovement == false)
        {
            _jumpGrace = 0;

            // Jump Cooldown
            _stateController.IgnoreJumpInputTime = Time.time + 0.5f;

            //Add force
            _rb.velocity = new Vector3(_stateController._Rb.velocity.x * _jumpForceVelocityMult, 0, _stateController._Rb.velocity.z * _jumpForceVelocityMult);
            _rb.AddForce(_jumpForceUp * jumpMult * Vector3.up, ForceMode.Impulse);

            // Anim
            _stateController._modelController.AddCrouching(1, 0.1f, 0.05f);

            // Shake
            _stateController._CameraShake.PlayShake(1.0f, 0.4f, 0.2f, 0.4f);

            // Sound
            _stateController._Sound.JumpSound(0.0f);
        }
    }

    private void Move()
    {
        //Move Vector
        Vector3 move = _stateController.moveInput * Time.deltaTime * _moveAcceleration * speedMult * inputInfluence;

        //Moving the player
        Vector3 horizontalVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        float nextMagnitudeWithInput = (horizontalVelocity + move * Time.deltaTime).magnitude;

        if (horizontalVelocity.magnitude < maxSpeed * speedMult || nextMagnitudeWithInput <= horizontalVelocity.magnitude)
        {
            _rb.AddForce(move);
        }

        _stateController._modelController.acceleration = move.normalized;
        _stateController._modelController.onGround = _stateController.onGround;

        if (move.magnitude != 0)
            _stateController.LastMoveDirection = new Vector2(move.x, move.z).normalized;
    }
}
