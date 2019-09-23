using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody _Rb;
    private PlayerStateController _stateMachine;

    [Header("Movement")]
    public float moveSpeed = 1.0f;
    public float maxSpeed = 4.0f;
    [HideInInspector] public bool disableMovement = false;

    [Space]
    public float inputInfluenceGrounded = 1.0f;
    public float inputInfluenceInAir = 0.2f;
    private float inputInfluence = 1.0f;

    [Header("Jump")]
    public float jumpForceForward = 5;
    public float jumpForceUp = 4;

    [SerializeField] private float downForceRate = 1f;
    private float downForce = 0;
    [HideInInspector] public bool OnGround;

    [Header("Inputs")]
    [HideInInspector] public float horizontalInput = 0;
    [HideInInspector] public float verticalInput = 0;

    [HideInInspector] public bool jumpInput = false;

    [HideInInspector] public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _stateMachine = GetComponent<PlayerStateController>();
    }

    void Update()
    {
        //Movement
        PlayerMove();

        //If controls are disabled
        if (disableMovement == true)
            return;
        
        //OnGround
        CheckGrounded();

        //Jump
        ArchJump();
    }

    private void ArchJump()
    {
        if (jumpInput)
        {
            if (OnGround)
            {
                //Getting Jump direction
                Vector3 jumpVector = new Vector3(_stateMachine.movementDir.x, 0, _stateMachine.movementDir.z).normalized;

                //Animation
                _stateMachine._animHandler.StartJump(jumpVector);

                //Adding force amounts
                jumpVector *= jumpForceForward;
                jumpVector.y = jumpForceUp * _Rb.mass;

                //Add force
                _Rb.AddForce(jumpVector, ForceMode.Impulse);
            }

            jumpInput = false;
        }
    }

    private void PlayerMove()
    {
        //Getting Input
        float translation = verticalInput;
        float straffe = horizontalInput;

        //Move Vector
        Vector3 move = new Vector3(straffe, 0, translation);
        move = _stateMachine._Camera.TransformDirection(move);
        move = new Vector3(move.x, 0, move.z);
        move = move.normalized;
            
        moveDirection = move;

        if (disableMovement == false)
        {
            //Moving the player
            move = move * Time.deltaTime * moveSpeed * inputInfluence * _Rb.mass;
            if (new Vector3(_Rb.velocity.x, 0, _Rb.velocity.z).magnitude < maxSpeed * inputInfluence)
                _Rb.AddForce(move);
        }
    }

    private void CheckGrounded()
    {
        if (OnGround)
        {
            inputInfluence = inputInfluenceGrounded;
            downForce = 0;
        }
        else
        {
            inputInfluence = inputInfluenceInAir;
            downForce += downForceRate * Time.deltaTime;
        }
        
        _Rb.AddForce(Vector3.down * Mathf.Pow(downForce, 2) * _Rb.mass);
    }
}
