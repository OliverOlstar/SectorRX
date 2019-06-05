using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V1 : MonoBehaviour
{
    // Rigidbody
    public Rigidbody rb;

    // Movement Variables
    public float moveSpeed = 1.0f;
    public bool disableMove;

    // Jump Variables
    public bool regJump;
    public bool archJump;
    public int jumpForce = 5;
    private int jumpLayer = 10;
    private bool isGrounded;

    void Start()
    {
        // rb as Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Enable/Disable Movement
        if (!disableMove)
        {
            PlayerMove();
        }
        if (disableMove)
        {
            return;
        }

        // Jump Update
        if (Input.GetButtonDown("Jump") && regJump)
        {
            RegularJump();
        }
        if (Input.GetButtonDown("Jump") && archJump)
        {
            ArchJump();
        }
    }

    void RegularJump()
    {
        if (isGrounded)
        {
            // Adding jump force to the rigidbody
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    void ArchJump()
    {
        if (isGrounded)
        {
            // Disabling player movement & adding jump forward & upward force
            rb.AddForce(0, jumpForce, jumpForce, ForceMode.Impulse);
        }
    }   

    void PlayerMove()
    {
        //Getting Input
        float translation = Input.GetAxis("Vertical");
        float straffe = Input.GetAxis("Horizontal");

        //Move Vector
        Vector3 move = new Vector3(straffe, 0, translation);
        move = Camera.main.transform.TransformDirection(move);
        move = new Vector3(move.x, 0, move.z);
        move = move.normalized * Time.deltaTime * moveSpeed;

        //Moving the player
        transform.Translate(move);
    }

    // Checking if player is colliding with jumpable layer #10
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == jumpLayer)
        {
            isGrounded = true;
            if (archJump)
            {
                disableMove = false;
            }
        }
    }

    // Checking if player is no longer colliding with jumpable layer #10
    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.layer == jumpLayer)
        {
            isGrounded = false;
            if (archJump)
            {
                disableMove = true;
            }
        }
    }
}
