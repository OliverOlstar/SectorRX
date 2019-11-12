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
    public float jumpDistance = 5;
    public float jumpDuration = 5;
    public float jumpForceUp = 4;

    [Space]
    [SerializeField] private float downForceRate = 1f;
    [SerializeField] private float downForceTerminal = 5f;
    private float downForce = 0;
    [HideInInspector] public bool OnGround;

    [Header("Inputs")]
    [HideInInspector] public float horizontalInput = 0;
    [HideInInspector] public float verticalInput = 0;
    [HideInInspector] public Vector3 moveDirection;

    [HideInInspector] public bool jumpInput = false;
    
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
        Jump();
    }

    private void Jump()
    {
        if (jumpInput)
        {
            if (OnGround)
            {
                //Jump Animation
                Vector3 jumpVector = new Vector3(_stateMachine.movementDir.x, 0, _stateMachine.movementDir.z).normalized;
                _stateMachine._animHandler.StartJump(jumpVector);

                //Add force
                _Rb.AddForce(jumpForceUp * _Rb.mass * Vector3.up, ForceMode.Impulse);
                StartCoroutine("JumpRoutine", jumpVector * (jumpDistance / jumpDuration) * _Rb.mass);
            }

            jumpInput = false;
        }
    }

    IEnumerator JumpRoutine(Vector3 pJumpDir)
    {
        float timer = 0;

        //Jump On An Arc
        while (timer <= jumpDuration && disableMovement == false && (OnGround == false || timer <= 0.2f))
        {
            _Rb.velocity = new Vector3(pJumpDir.x, _Rb.velocity.y, pJumpDir.z);
            yield return null;
            timer += Time.deltaTime;
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
        //Change the amount of influence player input has on the player movement based on wether he is grounded or not
        if (OnGround)
        {
            inputInfluence = inputInfluenceGrounded;
            downForce = 0;
        }
        else
        {
            inputInfluence = inputInfluenceInAir;
            //Add force downwards which adds ontop of gravity
            if (downForce < downForceTerminal)
                downForce += downForceRate * Time.deltaTime;
            else
                downForce = downForceTerminal;
        }
        
        _Rb.AddForce(Vector3.down * Mathf.Pow(downForce, 2) * _Rb.mass);
    }
}
