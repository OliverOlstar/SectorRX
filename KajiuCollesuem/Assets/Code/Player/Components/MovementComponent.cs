using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private PlayerStateController _StateController;

    [Header("Movement")]
    [SerializeField] private float _moveAcceleration = 1.0f;
    public float maxSpeed = 4.0f;

    [HideInInspector] public float inputInfluence = 1.0f;

    [Header("Jump")]
    [SerializeField] private float _jumpForceUp = 4;
    [SerializeField] private float _jumpForceVelocityMult = 1;

    [HideInInspector] public bool OnGround;

    public bool disableMovement = false;

    void Start()
    {
        _StateController = GetComponent<PlayerStateController>();
    }

    void Update()
    {
        if (disableMovement == true) 
            return;

        //Movement
        Move();
    }

    private void Move()
    {
        //Move Vector
        Vector3 move = new Vector3(_StateController.moveInput.x, 0, _StateController.moveInput.y);
        move = _StateController._Camera.TransformDirection(move);
        move.y = 0;
        move = move.normalized * Time.deltaTime * _moveAcceleration * inputInfluence;

        //Moving the player
        if (new Vector3(_StateController._rb.velocity.x, 0, _StateController._rb.velocity.z).magnitude < maxSpeed)
            _StateController._rb.AddForce(move);

        if (move.magnitude != 0)
            _StateController.LastMoveDirection = new Vector2(move.x, move.z).normalized;
    }

    private void OnJump()
    {
        if (OnGround && disableMovement == false)
        {
            //Add force
            _StateController._rb.velocity = new Vector3(_StateController._rb.velocity.x * _jumpForceVelocityMult, 0, _StateController._rb.velocity.z * _jumpForceVelocityMult);
            _StateController._rb.AddForce(_jumpForceUp * Vector3.up, ForceMode.Impulse);
            _StateController._animHandler.StartJump();
        }
    }
}
