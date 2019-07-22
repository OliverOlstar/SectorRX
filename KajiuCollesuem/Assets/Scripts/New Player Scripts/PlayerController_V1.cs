using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V1 : MonoBehaviour
{
    // Rigidbody
    private Rigidbody _Rb;
    private Transform _Camera;

    // Movement Variables
    public float moveSpeed = 1.0f;
    public bool disableMove;

    // Jump Variables
    public bool regJump;
    public int jumpForce = 5;
    private int jumpLayer = 11;
    public bool isGrounded;

    void Start()
    {
        // rb as Rigidbody
        _Rb = GetComponent<Rigidbody>();
        _Camera = Camera.main.transform;
    }

    void Update()
    {
        // Enable/Disable Movement
        if (disableMove)
        {
            return;
        }

        PlayerMove();

        // Jump Update
        if (Input.GetButtonDown("Jump"))
        {
            if (regJump)
            {
                RegularJump();
            }
            else
            {
                ArchJump();
            }
        }
    }

    void RegularJump()
    {
        if (isGrounded)
        {
            // Adding jump force to the rigidbody
            _Rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    void ArchJump()
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

    void PlayerMove()
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

    // Checking if player is colliding with jumpable layer #10
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == jumpLayer)
        {
            isGrounded = true;
        }
    }

    // Checking if player is no longer colliding with jumpable layer #10
    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.layer == jumpLayer)
        {
            isGrounded = false;
        }
    }

    private IEnumerator disablePlayerControls()
    {
        do
        {
            disableMove = true;
            yield return new WaitForSeconds(0.1f);
        } while (!isGrounded);

        disableMove = false;
    }
}
