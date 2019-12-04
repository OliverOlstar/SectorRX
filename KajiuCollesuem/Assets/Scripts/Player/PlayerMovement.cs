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

    [HideInInspector] public float targetInputInfluence = 1.0f;
    [Space] [SerializeField] private float influenceUpdateSpeed = 1.0f;
    [HideInInspector] public float inputInfluence = 1.0f;

    [Header("Jump")]
    public float jumpDistance = 5;
    public float jumpDuration = 5;
    public float jumpForceUp = 4;

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

        //Update inputInflunce to target
        inputInfluence = Mathf.Lerp(inputInfluence, targetInputInfluence, influenceUpdateSpeed * Time.deltaTime);
    }

    public void EndJump()
    {
        StopCoroutine("JumpRoutine");
    }
}
