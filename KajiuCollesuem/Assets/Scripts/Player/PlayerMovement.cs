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
    [HideInInspector] public bool disableMovement = false;

    [Space]
    public float inputInfluenceGrounded = 1.0f;
    public float inputInfluenceInAir = 0.2f;
    private float inputInfluence = 1.0f;

    [Header("Jump")]
    public float jumpForceForward = 5;
    public float jumpForceUp = 4;

    [Space]
    public float isGroundedCheckDistance = 1.0f;
    private bool isGrounded;

    [SerializeField] private float downForceRate = 1f;
    private float downForce = 0;

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
        ArchJump();
    }

    private void CheckGrounded()
    {
        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.1f, false);

        //Raycast to check for if grounded
        if (Physics.Raycast(transform.position, Vector3.down, isGroundedCheckDistance))
        {
            inputInfluence = inputInfluenceGrounded;
            isGrounded = true;
            downForce = 0;
        }
        else
        {
            inputInfluence = inputInfluenceInAir;
            isGrounded = false;
            downForce += downForceRate * Time.deltaTime;
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
        move = _Camera.TransformDirection(move);
        move = new Vector3(move.x, 0, move.z);
        move = move.normalized * Time.deltaTime * moveSpeed * inputInfluence * _Rb.mass;
        
        //Moving the player
        if (new Vector3(_Rb.velocity.x, 0, _Rb.velocity.z).magnitude < maxSpeed * inputInfluence)
            _Rb.AddForce(move);

        _Rb.AddForce(Vector3.down * Mathf.Pow(downForce, 2) * _Rb.mass);
    }
}
