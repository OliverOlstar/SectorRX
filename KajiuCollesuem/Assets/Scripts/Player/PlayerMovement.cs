using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody _Rb;
    private Transform _Camera;

    [Header("Movement")]
    public float moveSpeed = 1.0f;
    public float maxSpeed = 4.0f;
    public bool disableMovement = false;

    [Space]
    public float inputInfluence = 1.0f;
    public float inputInfluenceGrounded = 1.0f;
    public float inputInfluenceInAir = 0.2f;

    [Header("Jump")]
    public bool regJump;
    public float jumpForceForward = 5;
    public float jumpForceUp = 4;

    [Space]
    public bool isGrounded;
    public float isGroundedCheckDistance = 1.0f;

    [Header("Inputs")]
    [HideInInspector] public float horizontalInput = 0;
    [HideInInspector] public float verticalInput = 0;

    [HideInInspector] public bool jumpInput = false;

    // Start is called before the first frame update
    void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Camera = Camera.main.transform;
    }

    void Update()
    {
        //If controls are disabled
        if (disableMovement == true)
            return;

        //IsGrounded
        CheckGrounded();

        //Movement
        PlayerMove();

        //Jump
        if (regJump) { RegularJump(); } else { ArchJump(); }
    }

    private void CheckGrounded()
    {
        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.1f, false);

        //Raycast to check for if grounded
        if (Physics.Raycast(transform.position, Vector3.down, isGroundedCheckDistance))
        {
            inputInfluence = inputInfluenceGrounded;
            isGrounded = true;
        }
        else
        {
            inputInfluence = inputInfluenceInAir;
            isGrounded = false;
        }
    }

    private void RegularJump()
    {

        if (jumpInput)
        {
            if (isGrounded)
            {
                // Adding jump force to the rigidbody
                _Rb.AddForce(0, jumpForceUp, 0, ForceMode.Impulse);
            }

            jumpInput = false;
        }
    }

    private void ArchJump()
    {
        if (jumpInput)
        {
            if (isGrounded)
            {
                //Getting Jump direction
                Vector3 jumpVector = _Camera.parent.TransformDirection(Vector3.forward);

                //Setting Force forward and up
                jumpVector.y = 0;
                jumpVector = jumpVector.normalized * jumpForceForward;
                jumpVector.y = jumpForceUp;

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
        move = _Camera.TransformDirection(move);
        move = new Vector3(move.x, 0, move.z);
        move = move.normalized * Time.deltaTime * moveSpeed * inputInfluence;
        
        //Moving the player
        if (_Rb.velocity.magnitude < maxSpeed * inputInfluence)
            _Rb.AddForce(move);
    }
}
