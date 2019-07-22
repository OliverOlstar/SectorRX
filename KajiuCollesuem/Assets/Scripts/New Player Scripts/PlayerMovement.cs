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
    public bool disableMovement = false;
    public float inputInfluence = 1.0f;

    [Header("Jump")]
    public bool regJump;
    public int jumpForce = 5;

    public bool isGrounded;
    public float isGroundedCheckDistance = 3;

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
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.1f, true);

        if (Physics.Raycast(transform.position, Vector3.down, isGroundedCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void RegularJump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // Adding jump force to the rigidbody
                _Rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }

    private void ArchJump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // Disabling player movement & adding jump forward & upward force
                Vector3 jumpVector = _Camera.parent.TransformDirection(Vector3.forward * jumpForce);
                jumpVector.y = 0;
                jumpVector = jumpVector.normalized * jumpForce;

                jumpVector.y = jumpForce;
                _Rb.AddForce(jumpVector, ForceMode.Impulse);

                StartCoroutine("disablePlayerControls");
            }
        }
    }

    private void PlayerMove()
    {
        //Getting Input
        float translation = Input.GetAxis("Vertical");
        float straffe = Input.GetAxis("Horizontal");

        //Move Vector
        Vector3 move = new Vector3(straffe, 0, translation);
        move = _Camera.TransformDirection(move);
        move = new Vector3(move.x, 0, move.z);
        move = move.normalized * Time.deltaTime * moveSpeed;

        //Moving the player
        transform.Translate(move);
    }
}
